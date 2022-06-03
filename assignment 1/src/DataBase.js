import contactPicture from './Pictures/contactPicture.png'
import ShlomysPicture from './Pictures/ShlomysPicture.png'
import chatManagement from './chatManagement.js';
import recording from './Pictures/recording.mp3';
import pi from './Pictures/pi.txt';
import video from './Pictures/goodvideo.mp4';

const users = [{ username: "Shlomy", password: "Shlomy123", name: "Shlomy", picture: contactPicture, friends: [], last: "" },
{ username: "Shabat", password: "Shabat123", name: "Shabat", picture: contactPicture, friends: [], last: "" },
{ username: "Shalom", password: "Shalom123", name: "Shalom", picture: contactPicture, friends: [], last: "" },
{ username: "Wumevorah", password: "Wumevorah123", name: "Wumevorah", picture: contactPicture, friends: [], last: "" },
{ username: "Alice", password: "Alice123", name: "Alice", picture: contactPicture, friends: [], last: "" },
{ username: "Bob", password: "Bob123", name: "Bob", picture: contactPicture, friends: [], last: "" },
{ username: "Eve", password: "Eve123", name: "Eve", picture: contactPicture, friends: [], last: "" },
{ username: "Teodor", password: "Teodor123", name: "teodor", picture: contactPicture, friends: [], last: "" }];

export function connect(i, j) {
    users[i].friends.push({ friend: users[j], chat: [] });
    users[j].friends.push({ friend: users[i], chat: [] });
}

export function initUsers() {
    connect(0, 1);
    connect(0, 2);
    connect(0, 3);
    connect(0, 4);
    connect(0, 5);
    connect(1, 3);
    connect(1, 4);
    connect(1, 5);
    connect(1, 6);
    connect(2, 3);
    connect(2, 4);
    connect(2, 6);
    connect(2, 7);
    connect(3, 5);
    connect(3, 7);
    connect(4, 6);
    connect(4, 7);
    connect(5, 6);
    connect(5, 7);
    connect(6, 7);
    for (let i = 0; i < 5; i++) {
        users[0].friends[i].chat.push(chatManagement.chatElement("Hello, my name is Anton and I like Hot Potato!", true, "text"));
        users[0].friends[i].chat.push(chatManagement.chatElement("<img src=\"" + ShlomysPicture + "\" style=\"max-width:30vh\"><br><br>", true, "image"));
        users[0].friends[i].chat.push(chatManagement.chatElement("<audio src=\"" + recording + "\" style=\"max-width:30vh;\" controls></audio>", true, "audio"));
        users[0].friends[i].chat.push(chatManagement.chatElement("<a href=\"" + pi + "\" download=\"pi.txt\">pi.txt</a>", true, "file"));
        //users[0].friends[i].chat.push(chatManagement.chatElement("<video width=\"400vw\" src=\"" + video + "\" controls></video>", true, "video"));
        let friend = users[0].friends[i].friend;
        let j = 0;
        for (j = 0; j < friend.friends.length; j++) {
            if (friend.friends[j].friend === users[0]) {
                break;
            }
        }
        friend.friends[j].chat.push(chatManagement.chatElement("Hello, my name is Anton and I like Hot Potato!", false, "text"));
        friend.friends[j].chat.push(chatManagement.chatElement("<img src=\"" + ShlomysPicture + "\" style=\"max-width:30vh\"><br><br>", false, "image"));
        friend.friends[j].chat.push(chatManagement.chatElement("<audio src=\"" + recording + "\" style=\"max-width:30vh;\" controls></audio>", false, "audio"));
        friend.friends[j].chat.push(chatManagement.chatElement("<a href=\"" + pi + "\" download=\"pi.txt\">pi.txt</a>", false, "file"));
        //friend.friends[j].chat.push(chatManagement.chatElement("<video width=\"400vw\" src=\"" + video + "\" controls></video>", false, "video"));
    }
}

export default users;