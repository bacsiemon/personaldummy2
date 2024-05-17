document.getElementById('loginForm').addEventListener('submit', function(event) {
    event.preventDefault();

    // Create a new XMLHttpRequest object
    var xhr = new XMLHttpRequest();

    // Configure the request
    xhr.open('POST', 'http://localhost:5052/api/account/register');
    xhr.setRequestHeader('Content-Type', 'application/json');

    //debug
    console.log(JSON.stringify({
      username: document.getElementById('username').value,
      email: document.getElementById('email').value,
      password: document.getElementById('password').value,
      privateKey: document.getElementById('privatekey').value
    }), 


    // Send the request
    xhr.send(JSON.stringify({
        username: document.getElementById('username').value,
        password: document.getElementById('password').value,
        email: document.getElementById('email').value,
        privateKey: document.getElementById('privatekey').value
    },


    
  )));
  xhr.onreadystatechange = function() {
    if (xhr.readyState === XMLHttpRequest.DONE) {
      if (xhr.status === 200) {
        // If the request was successful, return true
        document.getElementById("errormessage").innerHTML = "Register successful. Redirecting to login page";
        window.location.replace("../../login/login.html");
      } else {
        document.getElementById("errormessage").innerHTML = "Register failed.";
        // If the request was not successful, return false
      }
    }
  }



  });

 