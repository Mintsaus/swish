"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var pin = "1234";

connection.on("ReceiveMessage", function (swishNr, amount, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    document.getElementById("swishNr").innerText = swishNr;
    document.getElementById("amount").innerText = amount;
    document.getElementById("message").innerText = msg;
});

connection.start().then(function () {
    connection.invoke("AddToGroup", pin);
}).catch(function (err) {
    return console.error(err.toString());
});

