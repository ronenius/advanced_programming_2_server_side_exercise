import users from "./DataBase";
import userIdx from "./UserIdx";
import contactUsername from "./contactUsername";
import searchMessageManagement from "./searchMessageManagement";
import { getContact } from "./apiCommunication";
async function updateChatBoard() {
    let friend = await getContact(contactUsername.value);
    document.getElementById("curContactBoard").style.visibility = "visible";
    document.getElementById("curContactPicture").src = friend.picture;
    document.getElementById("curContactName").innerHTML = friend.name;
    searchMessageManagement.resetMessageSearch();
}
export default updateChatBoard;