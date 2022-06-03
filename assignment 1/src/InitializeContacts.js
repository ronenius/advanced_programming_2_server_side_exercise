import userIdx from "./UserIdx";
import users from "./DataBase.js"
import addMessage from "./AddMessage";
import contactUsername from "./contactUsername";
import updateChatBoard from "./updateChatBoard"
import padding from "./chatPadding"
import chatManagement from "./chatManagement";
import {getContacts, curChat, getChat} from './apiCommunication';
export function addContactElement(friend) {
    return "<input type=\"radio\" class=\"d-block btn-check\" name=\"btnradio\" id=\"" + friend.username + "\" autocomplete=\"off\">\n<label class=\"btn btn-outline-primary\" for=\"" + friend.username + "\"><div class=\"row\"><div class=\"col-2\"><img src=\"" + friend.picture + "\" class=\"rounded-circle\" width=\"40px\" height=\"40px\" style=\"object-fit: cover;\"></div><div class=\"col\"><div class=\"container\"><div class=\"row\" style=\"display: flex; justify-content: center\">" + friend.name + "</div><div id=\"" + friend.username + "lastmessage\" class=\"row\" style=\"display: flex; justify-content: center\">" + friend.last + "</div></div></div><div class=\"col-2\" id=\"" + friend.username + "Time\"></div></div></label>\n";
}
async function initContact() {
    let friends = await getContacts();
    document.getElementById("Friends").innerHTML = "";
    for (let i = 0; i < friends.length; i++) {
        document.getElementById("Friends").innerHTML += addContactElement(friends[i]);
    }
    for (let i = 0; i < friends.length; i++) {
        await chatManagement.updateTime(friends[i]);
    }
    for (let i = 0; i < friends.length; i++) {
        let f = document.getElementById(friends[i].username);
        if(f!=null) {
            f.onclick = async function() {
                curChat.chat = await getChat(friends[i].username);
                chatManagement.displayChat(friends[i]);
                contactUsername.value = friends[i].username;
                await updateChatBoard();
                document.getElementById("sendingBoard").style.visibility = "visible";
            }
        }
    }
}
export default initContact;