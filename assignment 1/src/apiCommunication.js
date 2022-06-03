import users, { connect } from "./DataBase";
import userIdx from "./UserIdx.js"
import contactUsername from "./contactUsername";
import chatManager from "./chatManagement"
import initContact from "./InitializeContacts";
import curUser from "./CurrentUser";
import token from "./Token";
import $ from "jquery"
import Server from "./Server";
import {HubConnectionBuilder} from "@microsoft/signalr"
import contactPicture from './Pictures/contactPicture.png'
export async function getContacts() {
    var cont;
    await $.ajax({
        url: Server + "/api/contacts",
        type: 'GET',
        contentType: "application/json",
        data: {user: curUser.value},
        beforeSend: function (x) {
            x.setRequestHeader("Authorization", "Bearer " + token.value);
        },
        success: function (data) {
            cont = data;
            return getContactsHelper(cont);
        },
        error: function() {
            cont = null;
            return getContactsHelper(cont);
        },
        statusCode: {
            401: function () {
                window.location.href = "..";
            }
        }
    });
    if (cont === null)
        return [];
    let contacts = [];
    for (let i = 0; i < cont.length; i++) {
        contacts.push({ username: cont[i].id, name: cont[i].name, server: cont[i].server, last: cont[i].last, lastdate: cont[i].lastdate, picture: contactPicture});
    }
    return contacts;
}

function getContactsHelper(cont) {
    if (cont === null)
        return [];
    let contacts = [];
    for (let i = 0; i < cont.length; i++) {
        contacts.push({ username: cont[i].id, name: cont[i].name, server: cont[i].server, last: cont[i].last, lastdate: cont[i].lastdate, picture: contactPicture });    
    }
    return contacts;
}

export async function getContact(username) {
    let cont;
    await $.ajax({
        url: Server + "/api/contacts/" + username,
        type: 'GET',
        contentType: "application/json",
        data: {user:curUser.value},
        beforeSend: function(x) {
            x.setRequestHeader("Authorization", "Bearer " + token.value);
        },
        success: function (data) {
            cont = data;
        },
        error: function() {
            cont = null;
        },
        statusCode: {
            401: function () {
                window.location.href = "..";
            }
        }
    });
    if (cont===null)
        return {username:"",name:"",server:"",last:"", lastdate:"",picture:contactPicture};
    return {username:cont.id,name:cont.name,server:cont.server,last:cont.last, lastdate:cont.lastdate, picture: contactPicture};
}

export async function getChat(username) {
    let ans;
    await $.ajax({
        url: Server + "/api/contacts/" + username + "/messages",
        type: 'GET',
        contentType: "application/json",
        data: {user:curUser.value},
        beforeSend: function(x) {
            x.setRequestHeader("Authorization", "Bearer " + token.value);
        },
        success: function (data) {
            ans = data;
        },
        error: function() {
            ans = null;
        },
        statusCode: {
            401: function () {
                window.location.href = "..";
            }
        }
    });
    if (ans===null)
        return [];
    let newChat = [];
    for (let i = 0; i < ans.length; i++) {
        newChat.push(chatManager.chatElement(ans[i].content,ans[i].sent,"text",new Date(ans[i].created)))
    }
    return newChat;
}

export async function addNew(username, server, nickname) {
    $.ajax({
        url: "http://" + server + "/api/invitations",
        type: 'POST',
        contentType: "application/json",
        data: JSON.stringify({from:curUser.value, to:username, server: server}),
        success: async function (data) {
            await $.ajax({
                url: Server + "/api/contacts/",
                type: 'POST',
                contentType: "application/json",
                beforeSend: function(x) {
                    x.setRequestHeader("Authorization", "Bearer " + token.value);
                },
                data: JSON.stringify({id:username, name:nickname, server: server, user:curUser.value}),
                success: async function (data) {},
                error: function() {
                    alert("Contact already exists");
                },
                statusCode: {
                    401: function () {
                        window.location.href = "..";
                    }
                }
            });
        },
        error: function() {
            alert("Error ecurred in the given server");
        }
    });
}
export async function sendNewMessage(username, message) {
    let cont = await getContact(username);
    if (cont.server !== Server.substr(7,Server.length)) {
        $.ajax({
            url: "http://" + cont.server + "/api/transfer",
            type: 'POST',
            contentType: "application/json",
            data: JSON.stringify({from:curUser.value, to:username, content: message}),
            success: async function (data) {
                await $.ajax({
                    url: Server + "/api/contacts/" + username + "/messages",
                    type: 'POST',
                    contentType: "application/json",
                    beforeSend: function(x) {
                        x.setRequestHeader("Authorization", "Bearer " + token.value);
                    },
                    data: JSON.stringify({content: message, user:curUser.value}),
                    success: async function (data) {},
                    error: function() {},
                    statusCode: {
                        401: function () {
                            window.location.href = "..";
                        }
                    }
                });
            },
            error: function() {
                alert("Error ecurred in the given server");
            }
        });
    }
    else {
        await $.ajax({
            url: Server + "/api/contacts/" + username + "/messages",
            type: 'POST',
            contentType: "application/json",
            beforeSend: function(x) {
                x.setRequestHeader("Authorization", "Bearer " + token.value);
            },
            data: JSON.stringify({content: message, user:curUser.value}),
            success: async function (data) {},
            error: function() {},
            statusCode: {
                401: function () {
                    window.location.href = "..";
                }
            }
        });
    }
}
let inited = false;
export async function initHubConnection() {
    if (inited)
        return;
    var connection = new HubConnectionBuilder().withUrl(Server + "/myHub").build();
    await connection.start();
    connection.on("NewMessage", async function() {
        if (contactUsername.value !== "")
            curChat.chat = await getChat(contactUsername.value);
        chatManager.displayChat(await getContact(contactUsername.value));
        let conts = await getContacts();
        for (let i = 0; i < conts.length; i++) {
            chatManager.updateTime(conts[i]);
        }
    });
    connection.on("NewContact", async function() {
        await initContact();
        document.getElementById(contactUsername.value).onclick();
    });
    inited = true;
}
var curChat = {chat:[]};
export {curChat};