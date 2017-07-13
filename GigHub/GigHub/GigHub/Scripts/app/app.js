/*******************************
* Revealing module pattern in JavaScript
********************************
var Person = function () {
    var firstName = "Kef";

    var sayHello = function() {
        console.log(firstName);
    }

    return {
        sayHello: sayHello
    }
}(); //() <- IIF - imediatelly invoked JavaScript function

Person.sayHello();
*/



