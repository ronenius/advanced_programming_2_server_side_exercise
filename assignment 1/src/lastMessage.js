function getLastMessage(chat) {
    if (chat.length === 0) {
        return "";
    }
    let lastMessage = chat[chat.length - 1];
    if (lastMessage.type === "text") {
        if (lastMessage.contance.length <= 20) {
            return lastMessage.contance;
        }
        else {
            return lastMessage.contance.substring(0, 19) + "...";
        }
    }
    if (lastMessage.type === "image") {
        return "image";
    }
    if (lastMessage.type === "file") {
        return "file";
    }
    if (lastMessage.type === "audio") {
        return "recording";
    }
    if (lastMessage.type === "video") {
        return "video";
    }
}

export default getLastMessage;