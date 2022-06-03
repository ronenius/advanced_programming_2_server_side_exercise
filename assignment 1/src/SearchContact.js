import users from './DataBase.js'
import userIdx from './UserIdx';
import addContactFunctions from "./AddContact.js"
import initContact from './InitializeContacts';
import { getContact, getContacts } from './apiCommunication.js';
async function applySearch(s) {
    await initContact();
    let contactPlaceHolder = document.getElementById("Friends");
    let searchedContacts = [];
    let children = contactPlaceHolder.children;
    for (let i = 0; i < children.length; i+=2) {
        let friend = await getContact(children[i].id);
        if (friend.name.toLowerCase().includes(s.toLowerCase())) {
            searchedContacts.push(children[i]);
            searchedContacts.push(children[i + 1]);
        }
    }
    let child = contactPlaceHolder.lastElementChild;
    while (child) {
        contactPlaceHolder.removeChild(child);
        child = contactPlaceHolder.lastElementChild;
    }
    for (let i = 0; i < searchedContacts.length; i++)
        contactPlaceHolder.appendChild(searchedContacts[i]);
}
function searchContactButton() {
    applySearch(document.getElementById("searchContact").value);
}
export default searchContactButton;