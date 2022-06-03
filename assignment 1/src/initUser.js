import users from "./DataBase";
import userIdx from "./UserIdx";
import initContact, { addContactElement } from "./InitializeContacts";

function initUser() {
    document.getElementById("Friends").innerHTML = "";
    for (let i = 0; i < users[userIdx.value].friends.length; i++) {
        document.getElementById("Friends").innerHTML += addContactElement(users[userIdx.value].friends[i]);
    }
    initContact();
}
