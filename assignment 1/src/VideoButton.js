import chatManagement from "./chatManagement";
import contactIdx from "./contactUsername";
import users from "./DataBase";
import userIdx from "./UserIdx";
import searchMessageManagement from "./searchMessageManagement";
function videoButton(event) {
    let files = event.target.files;
    let friends = users[userIdx.value].friends;
    if (files.length === 0)
        return;
    friends[contactIdx.value].chat.push(chatManagement.chatElement("<video width=\"400vw\" src=\""+URL.createObjectURL(files[0])+"\" controls></video>",true, "video"));
    let friend = friends[contactIdx.value].friend;
    let idx = 0;
    for (let i = 0; i < friend.friends.length; i++) {
        if (friend.friends[i].friend===users[userIdx.value]) {
            idx = i;
            break;
        }
    }
    friend.friends[idx].chat.push(chatManagement.chatElement("<video width=\"400vw\" src=\""+URL.createObjectURL(files[0])+"\" controls></video>",false, "video"));
    chatManagement.displayChat(friends[contactIdx.value]);
    chatManagement.updateTime(friends[contactIdx.value]);
    event.target.value=null;
    searchMessageManagement.resetMessageSearch();
}
export default videoButton;