import ConnectToMain from "./ConnectToMain.js";
import users from './DataBase.js';
import contactPicture from './Pictures/contactPicture.png'
import token from "./Token";
import $ from "jquery"
import Server from "./Server.js";

function CheckRegister(username, labelUsername, password, labelPassword, verifyPassword, labelVerifyPassword, nickname, labelNickname, imageSrc, validationText, navigate) {
    if (username.current.value.length === 0) {
        validationText.current.innerHTML = "Username must not be empty!";
        ErrorRegister(username, labelUsername, password, labelPassword, verifyPassword, labelVerifyPassword, nickname, labelNickname, validationText);
        return;
    }
    if (username.current.value.includes(";")) {
        validationText.current.innerHTML = "Username must not have the character ';'";
        ErrorRegister(username, labelUsername, password, labelPassword, verifyPassword, labelVerifyPassword, nickname, labelNickname, validationText);
        return;
    }
    if (nickname.current.value.length === 0) {
        validationText.current.innerHTML = "Nickname must not be empty!";
        ErrorRegister(username, labelUsername, password, labelPassword, verifyPassword, labelVerifyPassword, nickname, labelNickname, validationText);
        return;
    }
    if (password.current.value.length < 6) {
        validationText.current.innerHTML = "Password must have at least 6 letters!";
        ErrorRegister(username, labelUsername, password, labelPassword, verifyPassword, labelVerifyPassword, nickname, labelNickname, validationText);
        return;
    }
    let regex = /\w/;
    if (!regex.test(password.current.value)) {
        validationText.current.innerHTML = "Password must have a word character!";
        ErrorRegister(username, labelUsername, password, labelPassword, verifyPassword, labelVerifyPassword, nickname, labelNickname, validationText);
        return;
    }
    regex = /\d/;
    if (!regex.test(password.current.value)) {
        validationText.current.innerHTML = "Password must have a number!";
        ErrorRegister(username, labelUsername, password, labelPassword, verifyPassword, labelVerifyPassword, nickname, labelNickname, validationText);
        return;
    }
    if (password.current.value !== verifyPassword.current.value) {
        validationText.current.innerHTML = "Passwords don't match!";
        ErrorRegister(username, labelUsername, password, labelPassword, verifyPassword, labelVerifyPassword, nickname, labelNickname, validationText);
        return;
    }
    $.ajax({
        url: Server + "/api/register",
        type: 'POST',
        contentType: "application/json",
        data: JSON.stringify({username: username.current.value, password: password.current.value, name: nickname.current.value}),
        success: function (t) {
            token.value = t;
            ConnectToMain(username.current.value, password.current.value, navigate);
        },
        error: function() {
            ErrorRegister(username, labelUsername, password, labelPassword, verifyPassword, labelVerifyPassword, nickname, labelNickname, validationText);
        }
    });
}

function ErrorRegister(username, labelUsername, password, labelPassword, verifyPassword, labelVerifyPassword, nickname, labelNickname, validationText) {
    labelUsername.current.style.color = "red";
    username.current.style.boxShadow = "none";
    username.current.style.borderWidth = "2px";
    username.current.style.borderColor = "red";
    labelPassword.current.style.color = "red";
    password.current.style.boxShadow = "none";
    password.current.style.borderWidth = "2px";
    password.current.style.borderTopWidth = "0px";
    password.current.style.borderColor = "red";
    labelVerifyPassword.current.style.color = "red";
    verifyPassword.current.style.boxShadow = "none";
    verifyPassword.current.style.borderWidth = "2px";
    verifyPassword.current.style.borderTopWidth = "0px";
    verifyPassword.current.style.borderColor = "red";
    labelNickname.current.style.color = "red";
    nickname.current.style.boxShadow = "none";
    nickname.current.style.borderWidth = "2px";
    nickname.current.style.borderTopWidth = "0px";
    nickname.current.style.borderColor = "red";
    nickname.current.style.marginBottom = "1vh";
    validationText.current.hidden = false;
}

export default CheckRegister;