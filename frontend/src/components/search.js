import { Users } from "../classes";
import { getMyApiUrl, fetchingData } from "../scripts/my_functions";

function DoFilter(srchval, data, typem = "all") {
    var res = "";
    
    switch (typem) {
        case "id": 
            res = data.filter(x => x.id == parseInt(srchval, 0));
            break;
        case "username":
            res = data.filter(x => x.username.toLowerCase().includes(srchval.toLowerCase()));
            break;
        case "email":
            res = data.filter(x => x.email.toLowerCase().includes(srchval.toLowerCase()));
            break;
        case "displayname":
            res = data.filter(x => x.displayname.toLowerCase().includes(srchval.toLowerCase()));
            break;
        case "role":
            res = data.filter(x => x.role.toLowerCase().includes(srchval.toLowerCase()));
            break;
        case "all":
        default:
            res = data.filter(x => 
                x.id == parseInt(srchval, 0) ||
                x.username.includes(srchval) || 
                x.email.includes(srchval) || 
                x.displayname.includes(srchval) ||
                x.role.includes(srchval)
            );
    }

    return res;
}

function DoSearch(srchval, isFocusIn = false) {
    const apiUrl = getMyApiUrl();
    var usersreslist = document.querySelector('#usersreslist');
    var mylogin = localStorage.getItem('login') ? JSON.parse(localStorage.getItem('login')) : null;

    if(usersreslist) {
        if(isFocusIn == true && srchval.length >= 0) {
            if(usersreslist.classList.contains("hidden")) {
                usersreslist.classList.remove("hidden");
            }

            if(!usersreslist.classList.contains("showin")) {
                usersreslist.classList.add("showin");
            }
        } else {
            if(usersreslist.classList.contains("showin")) {
                usersreslist.classList.remove("showin");
            }

            if(!usersreslist.classList.contains("hidden")) {
                usersreslist.classList.add("hidden");
            }
        }

        setTimeout(() => {
            fetchingData(`${apiUrl}/api/users`, "GET", null, mylogin.token).then(([users]) => {
                var musers = srchval.length >= 1 ? DoFilter(srchval, users, "all") : users;
                var usersres = JSON.parse(JSON.stringify(musers));
                var uid = 0; var uname = ""; var uavatar = ""; var userlinks = "";

                if(usersres != null && usersres.length > 0) {
                    if(srchval.length >= 0) {
                        usersreslist.innerHTML = "";
                        usersres.forEach(urx => {
                            uid = urx.id ?? 0;
                            uname = urx.firstName != null && urx.lastName != null ? `${urx.firstName}  ${urx.lastName}`: `${urx.username}`;
                            uavatar = urx.image.indexOf("/users") !== -1 ? urx.image.replace("/users", "") : urx.image; 
                            
                            userlinks += `
                            <li class="mt-3" style="list-style-type: none;">
                                <a href="pages/profile.html?id=${uid}" target="_blank" style="text-decoration: none;">
                                    <img src="${uavatar}"  class="img-avatar" width="50" height="50" loading="lazy" />
                                    <span class="userfullname ms-1">${uname}</span>
                                </a>
                            </li>`;
                        });

                        usersreslist.innerHTML = `
                        <label class="float-left t-left">Users</label>
                        <btn class="btn btn-primary btnclose" id="btnclose"><i class="bi bi-x"></i></btn>
                        <div class="clearfix"></div>
                        <ul class="srchresblk">${userlinks}</ul>`;
                    } else {
                        usersreslist.innerHTML = `
                        <label class="float-left t-left">Users</label>
                        <btn class="btn btn-primary btnclose" id="btnclose"><i class="bi bi-x"></i></btn>
                        <div class="clearfix"></div>
                        <ul class="srchresblk"><li><span>No users has been found with this search ${srchval}!</span></li></ul>`;
                    }
                } else {
                    usersreslist.innerHTML = `
                    <label class="float-left t-left">Users</label>
                    <btn class="btn btn-primary btnclose" id="btnclose"><i class="bi bi-x"></i></btn>
                    <div class="clearfix"></div>
                    <ul class="srchresblk"><li><span>No users has been found with this search ${srchval}!</span></li></ul>`;
                }

                if(document.querySelector('#usersreslist #btnclose')) {
                    document.querySelector('#usersreslist #btnclose').onclick = function(e) {
                        e.preventDefault();
                        document.querySelector('#inpsearch').value = "";
        
                        if(document.querySelector("#usersreslist").classList.contains('showin')) {
                            document.querySelector("#usersreslist").classList.remove("showin");
                        }
            
                        if(!document.querySelector("#usersreslist").classList.contains('hidden')) {
                            document.querySelector("#usersreslist").classList.add("hidden");
                        }
                    }
                }
            }).catch(err => console.log("Error while searching: " + err));
        }, 500);
    }
}

function Search() {
    var srchinp = document.querySelector('#inpsearch');
    var autofocus = false;
    var autoinput = true;

    if(srchinp) {
        if(autofocus) {
            srchinp.onfocus = function(e) {
                e.preventDefault();
                DoSearch(srchinp.value, true);
            }
    
            srchinp.onblur = function(e) {
                e.preventDefault();
                setInterval(() => {
                    DoSearch(srchinp.value, false);
                }, 10 * 1000);
            }
        }

        if(autoinput) {
            srchinp.oninput = function() {
                DoSearch(this.value, true);
            }
        }
    }

    if(document.querySelector('#frmsearch')) {
        document.querySelector('#frmsearch').onsubmit = function(e) {
            e.preventDefault();   

            if(!autoinput) {
                DoSearch(srchinp.value, true);
            }         
        }
    }
}

export { Search }