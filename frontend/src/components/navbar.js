const bootstrap = require('bootstrap');
const { doLogout } = require('../scripts/my_functions');

function Navbar() {
    if(document.querySelector('#mynavbarblk')) {
        var userdetails = localStorage.getItem("login");
        var useractionlinks = "";

        if(userdetails) {
            var jspdata = JSON.parse(userdetails);
            var myid = userdetails && jspdata != null ?  jspdata.usersId :  0;
            var myimage = userdetails && jspdata != null ? jspdata.image :  "../assets/images/guest.png";
            var mydisplayname = userdetails && jspdata != null ? jspdata.displayname : "Guest";

            myimage = myimage.indexOf('/users') !== -1 ? myimage.replace('/users', '') : myimage;

            useractionlinks = `
            <li class="nav-item">
                <div class="myfriendreqs" id="myfriendreqs">
                    <i class="bi bi-people-fill"></i>
                    <span class="counter">0</span>
                </div>
            </li>
            <li class="nav-item">
                <div class="mypm" id="mypm">
                    <i class="bi bi-chat-fill"></i>
                    <span class="counter">0</span>
                </div>
            </li>
            <li class="nav-item">
                <div class="mynotifications" id="mynotifications">
                    <i class="bi bi-bell-fill"></i>
                    <span class="counter">0</span>
                </div>
            </li>
            <li class="nav-item dropdown ms-auto me-0">
                <div class="myprofile">
                    <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                        <img src="${myimage}" alt="${mydisplayname}" class="img-fluid imgavatar" width="50" height="50" />
                    </a>
                    <ul class="dropdown-menu dropdown-menu-dark">
                        <li>
                            <a class="dropdown-item" href="pages/profile.html?id=${myid}">
                                <span class="uname" id="uname">${mydisplayname}</span>
                            </a>
                        </li>
                        <li>
                            <a class="dropdown-item settingslnk" href="pages/settings.html">
                                <i class="bi bi-gear me-1"></i>
                                Settings
                            </a>
                        </li>
                        <li>
                            <a class="dropdown-item logoutlnk" id="logoutlnk" href="#">
                                <i class="bi bi-door-closed me-1"></i>
                                Logout
                            </a>
                        </li>
                    </ul>
                </div>
            </li>`;
        } else {
            useractionlinks = `
            <li class="nav-item">
                <a href="pages/login.html" target="_self">
                    <i class="bi bi-person-fill-lock"></i>
                    <span>Login</span>
                </a>
            </li>`;
        }

        document.querySelector('#mynavbarblk').innerHTML = `
            <nav class="navbar navbar-expand-lg navbar-light bg-light my-col-top">
                <div class="container max-container">
                    <a class="navbar-brand" href="${userdetails ? "pages/main.html" : "/index.html"}">
                        <img src="assets/images/logo.svg" class="img-fluid img-logo" loading="lazy" style="width: 100%; height: 40px;" />
                    </a>
                    <button class="navbar-toggler d-lg-none" type="button" data-bs-toggle="collapse"
                        data-bs-target="#navPageLinks" aria-controls="navPageLinks" aria-expanded="false"
                        aria-label="Toggle navigation">
                        <span class="bi bi-list"></span>
                    </button>
                    <div class="collapse navbar-collapse" id="navPageLinks">
                        <div class="usersreslist hidden" id="usersreslist"></div>
                        <form action="pages/search.html" method="get" class="frmsearch d-flex ms-auto me-0 p-0 w-auto" id="frmsearch" role="search">
                            <input class="form-control inpsearch w-100 mw-100" id="inpsearch" type="search" placeholder="Search" aria-label="Search">
                            <button class="btn btn-primary btnsearch" type="submit">
                                <i class="bi bi-search"></i>
                            </button>
                        </form>
                        <ul class="navbar-nav nvuseractions ms-auto">
                           ${useractionlinks}
                        </ul>
                    </div>
                </div>
            </nav>
        `;

        doLogout();

        if(document.querySelector('#myfriendreqs')) {
            document.querySelector('#myfriendreqs').onclick = function() {
                if(document.querySelector('#modalmyfriendreqs')) {
                    const modal1 = new bootstrap.Modal('#modalmyfriendreqs');
                    modal1.show();
                }
            };
        }

        if(document.querySelector('#mypm')) {
            document.querySelector('#mypm').onclick = function() {
                if(document.querySelector('#modalmyprivatemessages')) {
                    const modal2 = new bootstrap.Modal('#modalmyprivatemessages');
                    modal2.show();
                }
            };
        }

        if(document.querySelector('#mynotifications')) {
            document.querySelector('#mynotifications').onclick = function() {
                if(document.querySelector('#modalmynotifications')) {
                    const modal3 = new bootstrap.Modal('#modalmynotifications');
                    modal3.show();
                }
            };
        }
    }
}

export { Navbar }