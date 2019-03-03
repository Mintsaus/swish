"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var globalPin = null;

connection.on("ReceiveMessage", function (swishNr, amount, message, img) {
    console.log(img);
    
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    document.getElementById("swishNr").innerText = swishNr;
    document.getElementById("amount").innerText = amount;
    document.getElementById("message").innerText = msg;
});

connection.start()
.then(function () {
})
.catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("pinButton").addEventListener("click", event => {
    var pin = document.getElementById("pinInput").value;
    console.log(pin);

    if(globalPin != null) {
        connection.invoke("RemoveFromGroup", globalPin);
    }
    globalPin = pin;
    connection.invoke("AddToGroup", globalPin);
});