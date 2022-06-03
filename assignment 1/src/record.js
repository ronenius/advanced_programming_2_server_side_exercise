import users from "./DataBase";
import userIdx from "./UserIdx";
import contactIdx from "./contactUsername"
import chatManagement from "./chatManagement"
import searchMessageManagement from "./searchMessageManagement"

/*let source = "";
let placeholder = document.getElementById("placeholder");
let recordSign = document.getElementById("recordSign");
let recordEndSign = document.getElementById("recordEndSign");*/
let mediaRecorder;
let recordedChunks = [];
let source = "";
let recordState = -1;
let record = function(stream) {
    let options = {mimeType: 'audio/webm'};
    mediaRecorder = new MediaRecorder(stream, options);
    mediaRecorder.addEventListener('dataavailable', function(e) {
        if (e.data.size > 0) recordedChunks.push(e.data);
    });
    mediaRecorder.addEventListener('stop', function() {
        source = URL.createObjectURL(new Blob(recordedChunks));
    });
    recordState = 0;
    startRecording();
};
function startRecording() {
    if (recordState === 0) {
        recordState=1;
        document.getElementById("recordSign").style.visibility = "visible";
        document.getElementById("recordingConfirm").disabled = true;
        document.getElementById("closeModalRecord").disabled = true;
        mediaRecorder.start();
    }
}
function startRecord() {
    if (recordState === -1)
        navigator.mediaDevices.getUserMedia({ audio: true, video: false }).then(record);
    else
        startRecording();
}
function stopRecord() {
    if (recordState === 1) {
        mediaRecorder.stop();
        recordState=2;
        document.getElementById("recordSign").style.visibility = "hidden";
        document.getElementById("recordEndSign").style.visibility = "visible";
        document.getElementById("recordingConfirm").disabled = false;
    }
}
function restartRecording() {
    if (recordState === 2) {
        recordedChunks = [];
        recordState = 0;
        document.getElementById("recordEndSign").style.visibility = "hidden";
        document.getElementById("closeModalRecord").disabled = false;
    }
}
function recordingConfirm() {
    let friends = users[userIdx.value].friends;
    if (recordState === 2) {
        document.getElementById("stopRecord").onclick();
        friends[contactIdx.value].chat.push(chatManagement.chatElement("<audio src=\""+source+"\" style=\"max-width:30vh;\" controls></audio>", true, "audio"));
        let friend = friends[contactIdx.value].friend;
        let idx = 0;
        for (let i = 0; i < friend.friends.length; i++) {
            if (friend.friends[i].friend===users[userIdx.value]) {
                idx = i;
                break;
            }
        }
        friend.friends[idx].chat.push(chatManagement.chatElement("<audio src=\""+source+"\" style=\"max-width:30vh;\" controls></audio>", false, "audio"));
        chatManagement.displayChat(friends[contactIdx.value]);
        document.getElementById("recordEndSign").style.visibility = "hidden";
        chatManagement.updateTime(friends[contactIdx.value]);
        searchMessageManagement.resetMessageSearch();
    }
    else if (recordState === 1) {
        mediaRecorder.stop();
        source = "";
        document.getElementById("recordSign").style.visibility = "hidden";
        recordState = 0;
        alert("Send while recording - the recording stopped and will be continued in the next record");
    }
    else {
        alert("Recording did not start, not sending");
        document.getElementById("stopRecord").onclick();
    }
    recordedChunks = [];
    source="";
    recordState = 0;
}
function closeModalRecord() {
    if (recordState === 1) {
        mediaRecorder.stop();
        source = "";
        document.getElementById("recordSign").style.visibility = "hidden";
        recordState = 0;
        alert("Exit while recording - the recording stopped and will be continued in the next record");
    }
    else if (recordState === 2){
        alert("Recording did not get sent - deleting record");
        /*source = "";
        recordedChunks = [];
        recordEndSign.style.visibility = "hidden";
        recordState = 0;*/
        document.getElementById("restartRecording").onclick();
    }
}

export default {startRecord, stopRecord, restartRecording, closeModalRecord, recordingConfirm};