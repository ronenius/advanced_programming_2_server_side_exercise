import users from './DataBase.js';
import { connect } from './DataBase';
import userIdx from './UserIdx';
import initContact from './InitializeContacts.js';
import getLastMessage from './lastMessage.js';
import {getContacts, addNew} from './apiCommunication';
import curUser from './CurrentUser.js';
const addContactButton = async function() {
    let friends = getContacts();
    let input1 = document.getElementById("inputAddContact");
    let input2 = document.getElementById("inputContactServer");
    let input3 = document.getElementById("inputContactNickname");
    for (let i = 0; i < friends.length; i++) {
        if (input1.value===friends[i].username) {
            alert("Contact already exists");
            input1.value="";
            input2.value="";
            input3.value="";
            return;
        }
    }
    if (input1.value===curUser.value) {
        alert("The username entered is yours");
        return;
    }
    await addNew(input1.value, input2.value, input3.value);
    input1.value = "";
    input2.value = "";
    input3.value = "";
    await initContact();
}
export const addContactModalExit = () => {
    let input1 = document.getElementById("inputAddContact");
    input1.value = "";
    let input2 = document.getElementById("inputContactServer");
    input2.value="";
}
export default {addContactButton, addContactModalExit};