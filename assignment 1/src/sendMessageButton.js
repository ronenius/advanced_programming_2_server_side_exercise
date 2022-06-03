import contactUsername from "./contactUsername";
import users from "./DataBase";
import userIdx from "./UserIdx";
import chatManager from "./chatManagement.js";
import searchMessageManagement from "./searchMessageManagement";
import { sendNewMessage } from "./apiCommunication";
import initContact from "./InitializeContacts";
import searchContactButton from "./SearchContact";
export async function sendMessage() {
    let input = document.getElementById("typeMessage").value;
    document.getElementById("typeMessage").value = "";
    if (input === "" || contactUsername.value === "")
        return;
    await sendNewMessage(contactUsername.value, input);
    await initContact();
    searchContactButton();
    document.getElementById(contactUsername.value).onclick();
    searchMessageManagement.resetMessageSearch();
}
export async function sendOnEnter(event) {
    if (event.code === "Enter") {
        await sendMessage();
    }
}
export default {sendMessage, sendOnEnter}