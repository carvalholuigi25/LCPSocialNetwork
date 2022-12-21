import { Users } from '../classes/users';

const isSSL = 1;
const headht = isSSL == 1 ? "https" : "http";
const port = isSSL == 1 ? 5001 : 5000;
const apiUrl = `${headht}://localhost:${port}`;

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

    // console.log(myInit);

    const [resp] = await Promise.all([
        fetch(url, myInit)
    ]);

     const aresp = await resp.json();
     return [aresp];
}

function getFormLogin() {
    if(document.querySelector('#mformlog')) {
        if(!localStorage.getItem('login')) {
            document.querySelector('#mformlog').innerHTML = `
            <form method="post" class="formlog mt-3" id="formlog">
                <div class="form-group mt-3" data-required="true">
                    <label for="username">Username</label>
                    <input type="text" name="username" id="username" class="form-control username" placeholder="Write your username here" />
                </div>
                <div class="form-group mt-3" data-required="true">
                    <label for="password">Password</label>
                    <input type="password" name="password" id="password" class="form-control password" placeholder="Write your password here" />
                </div>
                <div class="form-group mt-3 me-auto ms-auto text-center">
                    <div class="reqmsg mt-3 mb-3" id="reqmsg"></div>
                    <div class="forgetac mt-3" id="forgetac">
                        <p>Did you forget your account? Please <b><a href="pages/reset_account.html">click here</a></b> to reset your account.</p>
                    </div>
                    <input type="reset" class="btn btn-secondary btnclear ms-1" value="Clear" />
                    <input type="submit" class="btn btn-primary btnenter" value="Enter" />
                </div>
            </form>
            <a href="/pages/register.html" class="btn btn-primary mt-3">Register</a>
            <a href="/pages/main.html" class="btn btn-primary mt-3">Enter without login</a>`;
        }
        else 
        {
            document.querySelector('#mformlog').innerHTML = `
            <div class="warnblk d-flex flex-column justify-content-center align-items-center mt-3 mb-3">
                <i class="bi bi-exclamation-warning" style="font-size: 4rem;"></i>
                <h1 class="warningtitle mt-3">Warning</h1>
                <p class="warningmsg mt-3">User has already logged in!</p>
                <a href="/pages/main.html" class="btn btn-primary btnback mt-3">Back</a>
            </div>`;
        }
    }
}

function getFormReg() {
    if(document.querySelector('#mformreg')) {
        if(!localStorage.getItem('login')) {
            document.querySelector('#mformreg').innerHTML = `
            <form method="post" class="formreg mt-3" id="formreg">
                <div class="form-group mt-3" data-required="true">
                    <label for="username">Username</label>
                    <input type="text" name="username" id="username" class="form-control username" required placeholder="Write your username here" />
                </div>
                <div class="form-group mt-3" data-required="true">
                    <label for="email">Email</label>
                    <input type="email" name="email" id="email" class="form-control email" required placeholder="Write your email here" />
                </div>
                <div class="form-group mt-3" data-required="true">
                    <label for="password">Password</label>
                    <input type="password" name="password" id="password" class="form-control password" required placeholder="Write your password here" />
                </div>
                <div class="container">
                    <div class="row">
                        <div class="col-12 col-md-12 col-lg-6 form-group mt-3" data-required="true">
                            <label for="firstname">First Name</label>
                            <input type="text" name="firstname" id="firstname" class="form-control firstname" required placeholder="Write your first name here" />
                        </div>
                        <div class="col-12 col-md-12 col-lg-6 form-group mt-3" data-required="true">
                            <label for="lastname">Last Name</label>
                            <input type="text" name="lastname" id="lastname" class="form-control lastname" required placeholder="Write your last name here" />
                        </div>
                    </div>
                </div>
                <div class="form-group mt-3" data-required="true">
                    <label for="datebirthday">Date Birthday</label>
                    <input type="datetime-local" step="1" name="datebirthday" id="datebirthday" class="form-control datebirthday" value="1990-01-01T00:00:00" required placeholder="Write or select your date of birthday here" />
                </div>
                <div class="form-group mt-3" data-required="false">
                    <label for="cover">Cover (Image)</label>
                    <input type="file" name="cover" id="cover" class="form-control cover" placeholder="Upload your cover image here">
                    <button type="button" class="btn btn-primary mt-3 btnprevcover" id="btnprevcover">Preview my cover</button>
                    <figure class="blkprevcover mt-3 hidden" id="blkprevcover">
                        <img src="assets/images/default.webp" alt="guest" class="img-fluid coverprev" loading="lazy" />
                        <figcaption>The max is 1200px (width) x 300px (height)</figcaption>
                    </figure>
                </div>
                <div class="form-group mt-3" data-required="false">
                    <label for="avatar">Avatar (Image)</label>
                    <input type="file" name="image" id="image" class="form-control image" placeholder="Upload your avatar image here">
                    <button type="button" class="btn btn-primary mt-3 btnprevavatar" id="btnprevavatar">Preview my avatar</button>
                    <div class="blkprevavatar hidden" id="blkprevavatar">
                        <img src="assets/images/guest.png" alt="guest" class="img-fluid avatarprev mt-3" loading="lazy" />
                    </div>
                </div>
                <div class="form-group mt-3" data-required="true">
                    <input type="checkbox" name="accept_terms" id="accept_terms" class="accept_terms">
                    <label for="accept_terms">I read and accept the terms of service and privacy policy!</label>
                </div>
                <div class="form-group mt-3 me-auto ms-auto text-center">
                    <div class="reqmsg mt-3 mb-3" id="reqmsg"></div>
                    <input type="reset" class="btn btn-secondary btnclear ms-1" value="Clear" />
                    <input type="submit" class="btn btn-primary btnreg" value="Register" />
                </div>
            </form>
            <a href="/pages/login.html" class="btn btn-primary mt-3">Back to login</a>`;
        } else {
            document.querySelector('#mformreg').innerHTML = `
            <div class="warnblk d-flex flex-column justify-content-center align-items-center mt-3 mb-3">
                <i class="bi bi-exclamation-warning" style="font-size: 4rem;"></i>
                <h1 class="warningtitle mt-3">Warning</h1>
                <p class="warningmsg mt-3">User has already registered and logged in!</p>
                <a href="/pages/login.html" class="btn btn-primary btnback mt-3">Back</a>
            </div>`;
        }
    }
}

function doLogin() {
    getFormLogin();

    if(document.querySelector('#formlog')) {
        document.querySelector('#formlog').onsubmit = function(e) {
            e.preventDefault();

            var formData = {
                username: document.querySelector('#formlog #username').value,
                password: document.querySelector('#formlog #password').value
            };
    
            fetchingData(`${apiUrl}/api/users/auth/login`, "POST", formData).then(([authusers]) => {
                var jspdata = JSON.stringify(authusers);
                localStorage.setItem("login", jspdata);
                alert("User logged in!");
                
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
    getFormReg();

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
                cover: document.querySelector('#formreg #cover').value,
                image: document.querySelector('#formreg #image').value
            };

            fetchingData(`${apiUrl}/api/users/auth/register`, "POST", formData).then(([authusers]) => {
                alert("User registered!");
                setTimeout(() => {
                    window.location.href = "/pages/login.html";
                }, 1000 * 1);
            }).catch(err => console.log("Failed to register: " + err));
        };
    }
}

function doLogout() {
    if(document.querySelector('#logoutlnk')) {
        document.querySelector('#logoutlnk').onclick = function(e) {
            e.preventDefault();

            if(localStorage.getItem("login")) {
                localStorage.removeItem("login");
            }

            setTimeout(() => {
                alert("Logged out!");
                window.location.href = "/index.html";
            }, 1000 * 1);
        };
    }
}

function doReqMsgForFields() {
    if(document.querySelector('#reqmsg')) {
        document.querySelector('#reqmsg').innerHTML = `
        <p>
            <b>Note:</b> The field inputs marked in labels those with <span class="reqsym"></span> are required to fill!
        </p>`;
    }
}

function doPrevAvatar() {
    if(document.querySelector('#btnprevavatar')) {
        document.querySelector('#image').oninput = function(e) {
            document.querySelector('#blkprevavatar .avatarprev').src = "/assets/images/" + e.target.files[0].name;
        };
    
        document.querySelector('#btnprevavatar').onclick = function(e) {
            e.preventDefault();
            if(document.querySelector('#blkprevavatar').classList.contains('hidden')) {
                document.querySelector('#blkprevavatar').classList.remove('hidden');
            } else {
                document.querySelector('#blkprevavatar').classList.add('hidden');
            }
        };
    }
}

function doPrevCover() {
    if(document.querySelector('#btnprevcover')) {
        document.querySelector('#cover').oninput = function(e) {
            document.querySelector('#blkprevcover .coverprev').src = "/assets/images/" + e.target.files[0].name;
        };
    
        document.querySelector('#btnprevcover').onclick = function(e) {
            e.preventDefault();
            if(document.querySelector('#blkprevcover').classList.contains('hidden')) {
                document.querySelector('#blkprevcover').classList.remove('hidden');
            } else {
                document.querySelector('#blkprevcover').classList.add('hidden');
            }
        };
    }
}

export { importAll, doLogin, doReg, doLogout, doReqMsgForFields, doPrevAvatar, doPrevCover }