//debugger;
var getJwtBtn = document.getElementById('getJwt');
var testJwtBtn= document.getElementById('testJwt');

getJwtBtn.onclick = function () {
    var getRes = document.getElementById('getRes');
    getRes.innerText = "...........";
    // get token
    $.ajax({
        url: "http://localhost:57425/api/token",
        type: 'POST',
        data: '{ "username": "mario", "password": "secret" }',
        contentType: "application/json-patch+json",
        error: function (err) {
            console.log('Error!', err)
        },
        success: function (data) {
            console.log('Got token!')
            console.log('Token:' + data.token);

            getRes.innerText = "GotToken:" + data.token;

          
            // save token to localstorage 
            localStorage.setItem('token', data.token);
        }
    });
}

testJwtBtn.onclick = function () {
    //uses token to authenticate
    var testRes = document.getElementById('testRes');
    testRes.innerText = "...........";

    if (localStorage.getItem('token')) {
        $.ajax({
            url: "http://localhost:57425/api/values",
            type: 'GET',
            // Fetch the stored token from localStorage and set in the header
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'bearer ' + localStorage.getItem('token'));
            },
            contentType: "application/json-patch+json",
            error: function (err) {
                console.log('Error!', err)
            },
            success: function (data) {
                console.log('values' + data)
                testRes.innerText = "Authorised!......values:" + data;
            }
            // Fetch the stored token from localStorage and set in the header
        });
    } else {
        testRes.innerText = "valees:" + 'you need to get a token first';
    }
  
   

}


