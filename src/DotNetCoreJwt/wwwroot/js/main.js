//debugger;
var getJwtBtn = document.getElementById('getJwt');
var testJwtBtn= document.getElementById('testJwt');

getJwtBtn.onclick = function () {
    var getRes = document.getElementById('getRes');
    getRes.innerText = "...........";
    // get token
    $.ajax({

        url: "http://localhost:57425/api/Token?APIKey=SuperDuperApiKey",
        type: 'Post',
        data: '{"APIKey":"q1WkAk+jB3K1jc2cbwNDDO5JjwleCmUWhw/aPCay9J8="}',
        contentType: "application/json-patch+json",
        error: function (err) {
            console.log('Error!', err);
            getRes.innerText = "Error:" + err.statusCode + "        Status Text " + err.statusText;
        },
        success: function (data) {
            console.log('Got token!');
            console.log('Token:' + data.token);

            getRes.innerText = "GotToken:" + data.token;


            // save token to localstorage 
            localStorage.setItem('token', data.token);
        }
    });
};

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
                console.log('Error!', err.statusCode);
                testRes.innerText = "Error!" + ':Code:' + err.status + "        Status Text " + err.statusText + " ,your token has probably expired get a new one!!!";
            },
            success: function (data) {
                console.log('values' + data);
                testRes.innerText = "Authorised!......values:" + data;
            }
            // Fetch the stored token from localStorage and set in the header
        });
    } else {
        testRes.innerText = "valees:" + 'you need to get a token first';
    }



};

getApiKeyBtn.onclick = function () {
    var testRes = document.getElementById('getApiKeyRes');
    testRes.innerText = "...........";

        $.ajax({
            url: "http://localhost:57425/api/CreateApiKeys",
            type: 'GET',
            contentType: "application/json",
            error: function (err) {
                console.log('Error!', err.statusCode);
                testRes.innerText = "Error!" + ':Code:' + err.status + "        Status Text " + err.statusText + " ,failed to get API KEY!!!";
            },
            success: function (data) {
                console.log('values' + data);
                testRes.innerText = "New API Key: " + data;
            }
            // Fetch the stored token from localStorage and set in the header
        });
}
