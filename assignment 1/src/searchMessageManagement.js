import { curChat } from "./apiCommunication";
import contactUsername from "./contactUsername";
import users from "./DataBase";
import userIdx from "./UserIdx";
let messageIdx;
let messagesSearched = [];

function resetMessageSearch() {
    for (let i = 0; i < curChat.chat.length; i++) {
        if (curChat.chat[i].sent)
            document.getElementById(contactUsername.value+"message#"+i).children[0].children[0].children[1].style.background = "#c9ecc4";
        else
        document.getElementById(contactUsername.value+"message#"+i).children[0].children[0].children[0].children[1].style.background = "#c9ecc4";
    }
    messagesSearched = curChat.chat;
    document.getElementById("searchMessage").value = "";
    messageIdx = messagesSearched.length - 1;
    document.getElementById("prevResult").disabled=true;
    document.getElementById("nextResult").disabled=true;
}
function searchMessage() {
    let chat = curChat.chat;
    for (let i = 0; i < chat.length; i++) {
        if (chat[i].sent)
            document.getElementById(contactUsername.value + "message#" + i).children[0].children[0].children[1].style.background = "#c9ecc4";
        else
            document.getElementById(contactUsername.value + "message#" + i).children[0].children[0].children[0].children[1].style.background = "#c9ecc4";
    }
    messagesSearched = [];
    for (let i = 0; i < chat.length; i++) {
        if (chat[i].type==="text" && chat[i].contance.toLowerCase().includes(/*"message\">"+*/document.getElementById("searchMessage").value.toLowerCase())) {
            messagesSearched.push({obj:document.getElementById(contactUsername.value+"message#"+i),sent:chat[i].sent});
        }
    }
    messageIdx = messagesSearched.length - 1;
    document.getElementById("prevResult").disabled=false;
    document.getElementById("nextResult").disabled=false;
    if (messagesSearched.length!==0)
        document.getElementById("chatPlaceHolder").scrollTo(0,messagesSearched[messageIdx].obj.offsetTop);
}
function prevResult() {
    if (messagesSearched.length === 0)
        return;
    if (messagesSearched.length===1) {
        if (messagesSearched[0].sent) {
            messagesSearched[0].obj.children[0].children[0].children[1].style.backgroundColor = "yellow";
        }
        else {
            messagesSearched[0].obj.children[0].children[0].children[0].children[1].style.backgroundColor = "yellow";
        }
        return;
    }
    if (messageIdx!==0) {
        messageIdx--;
        if (messagesSearched[messageIdx + 1].sent)
            messagesSearched[messageIdx+1].obj.children[0].children[0].children[1].style.backgroundColor = "#c9ecc4";
        else
            messagesSearched[messageIdx+1].obj.children[0].children[0].children[0].children[1].style.backgroundColor = "#c9ecc4";
        if (messagesSearched[messageIdx].sent)
            messagesSearched[messageIdx].obj.children[0].children[0].children[1].style.backgroundColor = "yellow";
        else
            messagesSearched[messageIdx].obj.children[0].children[0].children[0].children[1].style.backgroundColor = "yellow";
        document.getElementById("chatPlaceHolder").scrollTo(0,messagesSearched[messageIdx].obj.offsetTop);
    }
}
function nextResult() {
    if (messagesSearched.length === 0)
        return;
    if (messagesSearched.length===1) {
        if (messagesSearched[0].sent) {
            messagesSearched[0].obj.children[0].children[0].children[1].style.backgroundColor = "yellow";
        }
        else {
            messagesSearched[0].obj.children[0].children[0].children[0].children[1].style.backgroundColor = "yellow";
        }
        return;
    }
    if (messageIdx!==messagesSearched.length - 1) {
        messageIdx++;
        if (messagesSearched[messageIdx - 1].sent)
            messagesSearched[messageIdx-1].obj.children[0].children[0].children[1].style.backgroundColor = "#c9ecc4";
        else
            messagesSearched[messageIdx-1].obj.children[0].children[0].children[0].children[1].style.backgroundColor = "#c9ecc4";
        if (messagesSearched[messageIdx].sent)
            messagesSearched[messageIdx].obj.children[0].children[0].children[1].style.backgroundColor = "yellow";
        else
            messagesSearched[messageIdx].obj.children[0].children[0].children[0].children[1].style.backgroundColor = "yellow";
        document.getElementById("chatPlaceHolder").scrollTo(0,messagesSearched[messageIdx].obj.offsetTop);
    }
}
export default {searchMessage,resetMessageSearch,prevResult, nextResult};