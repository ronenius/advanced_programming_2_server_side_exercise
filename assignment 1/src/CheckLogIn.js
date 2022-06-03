import ConnectToMain from "./ConnectToMain.js";
import users from "./DataBase.js";
import $ from "jquery"
import token from "./Token.js";
import Server from "./Server.js";

function CheckLogIn(username, labelUsername, password, labelPassword, validationText, navigate) {
    /*fetch("/api/login", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({username: username.current.value, password: password.current.value})
    }).then(function (response) {
        if (!response.ok) {
            ErrorLogIn(username, labelUsername, password, labelPassword, validationText);
            return;
        }
        //......
    });
    ConnectToMain(username.current.value, password.current.value, navigate);*/
    $.ajax({
        url: Server + "/api/login",
        type: 'POST',
        contentType: "application/json",
        data: JSON.stringify({username: username.current.value, password: password.current.value}),
        success: function (t) {
            token.value = t;
            ConnectToMain(username.current.value, password.current.value, navigate);
        },
        error: function() {
            ErrorLogIn(username, labelUsername, password, labelPassword, validationText);
        }
    });
}

function ErrorLogIn(username, labelUsername, password, labelPassword, validationText){
    labelUsername.current.style.color = "red";
    username.current.style.boxShadow = "none";
    username.current.style.borderWidth = "2px";
    username.current.style.borderColor = "red";
    labelPassword.current.style.color = "red";
    password.current.style.boxShadow = "none";
    password.current.style.borderWidth = "2px";
    password.current.style.borderTopWidth = "0px";
    password.current.style.borderColor = "red";
    password.current.style.marginBottom = "1vh";
    validationText.current.hidden = false;
}

export default CheckLogIn;