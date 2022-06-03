import users from "./DataBase";
import userIdx from "./UserIdx";
import contactIdx from "./contactUsername";
import chatManagement from "./chatManagement";
import searchMessageManagement from "./searchMessageManagement";
function addImageButton(event) {
    let files = event.target.files;
    let friends = users[userIdx.value].friends;
    for (let i = 0; i < files.length; i++) {
        let reader = new FileReader();
        reader.onload = function(e) {
            friends[contactIdx.value].chat.push(chatManagement.chatElement("<img src=\"" + e.target.result + "\" style=\"max-width:30vh\"><br><br>", true), "image");
            let friend = friends[contactIdx.value].friend;
            let idx = 0;
            for (let i = 0; i < friend.friends.length; i++) {
                if (friend.friends[i].friend===users[userIdx.value]) {
                    idx = i;
                    break;
                }
            }
            friend.friends[idx].chat.push(chatManagement.chatElement("<img src=\"" + e.target.result + "\" style=\"max-width:30vh\"><br><br>", false), "image");
            chatManagement.displayChat(friends[contactIdx.value]);
            chatManagement.updateTime(friends[contactIdx.value]);
            searchMessageManagement.resetMessageSearch();
        }
        reader.readAsDataURL(files[i]);
    }
    event.target.value=null;
};
export default addImageButton;