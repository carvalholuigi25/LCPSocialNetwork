import { Users } from '../classes/users';

//sources: 
// https://stackoverflow.com/a/42118921
// https://webpack.js.org/guides/asset-modules/
// https://webpack.js.org/guides/dependency-management/#requirecontext
function importAll(r) {
    return r.keys().map(r);
}

async function fetchingData(url = "/api/users/auth/login", method = "GET", body = null) {
    var mymthd = method == method.toLowerCase() ? method.toLowerCase() : method.toUpperCase();
    var myHeaders = new Headers();
    var myInit = { 
        method: method,
        headers: myHeaders,
        mode: 'cors',
        cache: 'default',
        body: mymthd != 'GET' || mymthd != 'get' ? body : null
    };

    const [resp] = await Promise.all([
        fetch(url, myInit)
    ]);

     const aresp = await resp.json();
     return [aresp];
}

function doLogin() {
    if(document.querySelector('#formlog')) {
        var formlog = document.forms.formlog;
        var formData = new FormData(formlog);

        fetchingData("/api/users/auth/login", "POST", formData).then(([authusers]) => {
            console.log(authusers);
            console.log("User logged in!");
            localStorage.setItem("login", authusers);
            setTimeout(() => {
                window.location.href = "/pages/main.html";
            }, 1000 * 1);
        }).catch(err => console.log("Failed to login: " + err));
    }
}

function doReg() {
    if(document.querySelector('#formreg')) {
        var formreg = document.forms.formreg;
        var formData = new FormData(formreg);

        fetchingData("/api/users/auth/register", "POST", formData).then(([authusers]) => {
            console.log(authusers);
            console.log("User registered!");
            setTimeout(() => {
                window.location.href = "/pages/login.html";
            }, 1000 * 1);
        }).catch(err => console.log("Failed to register: " + err));
    }
}

export { importAll, doLogin, doReg }