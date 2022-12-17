const bootstrap = require('bootstrap');

function Navbar() {
    if(document.querySelector('#mynavbarblk')) {
        document.querySelector('#mynavbarblk').innerHTML = `
            <nav class="navbar navbar-expand-lg navbar-light bg-light my-col-top">
                <div class="container max-container">
                    <a class="navbar-brand" href="/index.html">
                        <img src="assets/images/logo.svg" class="img-fluid img-logo" loading="lazy" style="width: 100%; height: 40px;" />
                    </a>
                    <button class="navbar-toggler d-lg-none" type="button" data-bs-toggle="collapse"
                        data-bs-target="#navPageLinks" aria-controls="navPageLinks" aria-expanded="false"
                        aria-label="Toggle navigation">
                        <span class="bi bi-list"></span>
                    </button>
                    <div class="collapse navbar-collapse" id="navPageLinks">
                        <form action="pages/search.html" method="get" class="frmsearch d-flex ms-auto me-0 p-0 w-auto" role="search">
                            <input class="form-control w-100 mw-100" type="search" placeholder="Search" aria-label="Search">
                            <button class="btn btn-primary btnsearch" type="submit">
                                <i class="bi bi-search"></i>
                            </button>
                        </form>
                        <ul class="navbar-nav nvuseractions ms-auto">
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
                            <li class="nav-item ms-1">
                                <div class="myprofile">
                                    <a href="pages/profile.html" target="_self">
                                        <img src="../assets/images/guest.png" alt="Guest" class="img-fluid imgavatar" width="50" height="50" />
                                    </a>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        `;

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