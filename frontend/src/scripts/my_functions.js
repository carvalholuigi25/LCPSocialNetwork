import { Users } from '../classes/users';

const apiUrl = "http://localhost:5000";
//sources: 
// https://stackoverflow.com/a/42118921
// https://webpack.js.org/guides/asset-modules/
// https://webpack.js.org/guides/dependency-management/#requirecontext
function importAll(r) {
    return r.keys().map(r);
}

async function fetchingData(url = "/api/users/auth/login", method = "GET", body = null) {
    var myHeaders = new Headers();
    var myInit = { 
        method: method,
        mode: 'cors',
        cache: 'no-cache',
        credentials: 'same-origin',
        headers:  {
            'Content-Type': 'application/json'
            // 'Content-Type': 'application/x-www-form-urlencoded',
        },
        redirect: 'follow',
        referrerPolicy: 'no-referrer',
        body: method != "GET" ? JSON.stringify(body) : null
    };

    console.log(myInit);

    const [resp] = await Promise.all([
        fetch(url, myInit)
    ]);

     const aresp = await resp.json();
     return [aresp];
}

function doLogin() {
    if(document.querySelector('#formlog')) {
        document.querySelector('#formlog').onsubmit = function(e) {
            e.preventDefault();

            var formData = {
                username: document.querySelector('#formlog #username').value,
                password: document.querySelector('#formlog #password').value
            };
    
            fetchingData(`${apiUrl}/api/users/auth/login`, "POST", formData).then(([authusers]) => {
                console.log(authusers);
                console.log("User logged in!");
                localStorage.setItem("login", JSON.stringify(authusers));
                if(localStorage.getItem("login")) {
                    setTimeout(() => {
                        window.location.href = "/pages/main.html";
                    }, 1000 * 1);
                }
            }).catch(err => console.log("Failed to login: " + err));
        };
    }
}

function doReg() {
    if(document.querySelector('#formreg')) {
        document.querySelector('#formlog').onsubmit = function(e) {
            e.preventDefault();

            var formData = {
                username: document.querySelector('#formreg #username').value,
                email: document.querySelector('#formreg #email').value,
                password: document.querySelector('#formreg #password').value,
                firstname: document.querySelector('#formreg #firstname').value,
                lastname: document.querySelector('#formreg #lastname').value,
                datebirthday: document.querySelector('#formreg #datebirthday').value,
                avatar: document.querySelector('#formreg #avatar').value,
                cover: document.querySelector('#formreg #cover').value
            };

            fetchingData(`${apiUrl}/api/users/auth/register`, "POST", formData).then(([authusers]) => {
                console.log(authusers);
                console.log("User registered!");
                setTimeout(() => {
                    window.location.href = "/pages/login.html";
                }, 1000 * 1);
            }).catch(err => console.log("Failed to register: " + err));
        };
    }
}

export { importAll, doLogin, doReg }