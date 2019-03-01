"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var globalPin = null;

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("messageSent", (message) => {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var li = document.createElement("li");
    li.textContent = "Meddelande lyckades " + msg;
    document.getElementById("messagesList").appendChild(li);
})

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var swishNr = document.getElementById("swishNrInput").value;
    var amount = document.getElementById("amountInput").value;
    var message = document.getElementById("messageInput").value;
    console.log(swishNr + amount + message + globalPin);
    connection.invoke("SendMessage", swishNr, amount, message, globalPin).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("pinButton").addEventListener("click", event => {
    var pin = document.getElementById("pinInput").value;
    console.log(pin);
    
    if(globalPin != null) {
        connection.invoke("RemoveFromGroup", globalPin);
    }
    globalPin = pin;
    connection.invoke("AddToGroup", globalPin);
    event.preventDefault();
});