document.getElementById('loginForm').addEventListener('submit', function(event) {
    event.preventDefault();

    // Create a new XMLHttpRequest object
    var xhr = new XMLHttpRequest();

    // Configure the request
    xhr.open('POST', 'http://localhost:5052/api/account/login');
    xhr.setRequestHeader('Content-Type', 'application/json');


    console.log(JSON.stringify({
      username: document.getElementById('username').value,
      password: document.getElementById('password').value
    }), 
    // Send the request
    xhr.send(JSON.stringify({
      username: document.getElementById('username').value,
      password: document.getElementById('password').value
    },


    xhr.onreadystatechange = function() {
      if (xhr.readyState === XMLHttpRequest.DONE) {
        if (xhr.status === 200) {
          // If the request was successful, return true
          return true;
        } else {
          // If the request was not successful, return false
          return false;
        }
      }
    }
  )));
  });

 