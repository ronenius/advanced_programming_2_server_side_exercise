import timeWriter from "./timeWriter.js"
import addMessage from "./AddMessage";
import padding from "./chatPadding"
import "./speechBubble.css";
import getLastMessage from "./lastMessage.js";
import { curChat } from "./apiCommunication.js";
import curUser from "./CurrentUser.js";
import contactUsername from "./contactUsername.js";
import { getContact } from "./apiCommunication.js";

function displayChat(contact) {
    let chat = curChat.chat;
    let PlaceHolder = document.getElementById("chatPlaceHolder");
    PlaceHolder.innerHTML="";
    for (let i = 0; i < chat.length; i++) {
        PlaceHolder.innerHTML+=addMessage(chat[i],contact.name, contact.username, i);
    }
    padding.addPadding(PlaceHolder);
    PlaceHolder.scrollTo(0,PlaceHolder.scrollHeight);
}
function chatElement(s, to, messageType, time = new Date()) {
    var x = {
        contance : s,
        hour:time.getHours(),
        minute:time.getMinutes(),
        day:time.getDate(),
        month:time.getMonth() + 1,
        year:time.getFullYear(),
        isoTime:time.toISOString(),
        sent:to,
        type:messageType
    };
    return x;
}   

async function updateTime(user) {
    let time = document.getElementById(user.username + "Time");
    let cont = await getContact(user.username);
    time.style.fontSize = "100%";
    if (cont.lastdate !== null)
        time.innerHTML = timeWriter.getISOtime(cont.lastdate) + "<br>" + timeWriter.getISOdate(cont.lastdate);
    let lastMessage = document.getElementById(user.username + "lastmessage");
    if (cont.last!==null)
        lastMessage.innerHTML = cont.last.substring(0, 19);
    else
        lastMessage.innerHTML = "";
}

export default { displayChat, chatElement, updateTime };