import { Users } from "../classes";
import { getMyApiUrl, fetchingData } from "../scripts/my_functions";

function DoSearch(srchval, isFocusIn = false) {
    const apiUrl = getMyApiUrl();
    var mylogin = localStorage.getItem('login') ? JSON.parse(localStorage.getItem('login')) : null;
    var usersreslist = document.querySelector('#usersreslist');

    if(usersreslist) {
        if(isFocusIn == true && srchval.length >= 0) {
            if(usersreslist.classList.contains("hidden")) {
                usersreslist.classList.remove("hidden");
            }

            if(!usersreslist.classList.contains("show")) {
                usersreslist.classList.add("show");
            }
        } else {
            if(usersreslist.classList.contains("show")) {
                usersreslist.classList.remove("show");
            }

            if(!usersreslist.classList.contains("hidden")) {
                usersreslist.classList.add("hidden");
            }
        }

        setTimeout(() => {
            fetchingData(`${apiUrl}/api/users`, "GET", null, mylogin.token).then(([users]) => {
                var musers = srchval.length >= 1 ? users.filter(x => 
                    x.username.includes(srchval.toLowerCase()) || 
                    x.email.includes(srchval.toLowerCase()) || 
                    x.firstName.includes(srchval.toLowerCase()) ||
                    x.lastName.includes(srchval.toLowerCase()) ||
                    x.displayname.includes(srchval.toLowerCase())
                ) : users;
                var usersres = JSON.parse(JSON.stringify(musers));
                var uid = 0;
                var uname = "";
                var uavatar = "";
                var userlinks = "";

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
                        <btn class="btn btn-primary btnclose" id="btnclose"><i class="bi bi-x"></i></btn>
                        <ul class="srchresblk">${userlinks}</ul>`;
                    } else {
                        usersreslist.innerHTML = `
                        <btn class="btn btn-primary btnclose" id="btnclose"><i class="bi bi-x"></i></btn>
                        <ul class="srchresblk"><li><span>No users has been found with this search ${srchval}!</span></li></ul>`;
                    }
                } else {
                    usersreslist.innerHTML = `
                    <btn class="btn btn-primary btnclose" id="btnclose"><i class="bi bi-x"></i></btn>
                    <ul class="srchresblk"><li><span>No users has been found with this search ${srchval}!</span></li></ul>`;
                }

                if(document.querySelector('#usersreslist #btnclose')) {
                    document.querySelector('#usersreslist #btnclose').onclick = function(e) {
                        e.preventDefault();
                        document.querySelector('#inpsearch').value = "";
        
                        if(document.querySelector("#usersreslist").classList.contains('show')) {
                            document.querySelector("#usersreslist").classList.remove("show");
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

    if(srchinp) {
        if(autofocus) {
            console.log("Autofocus is enabled!");

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
        } else {
            console.log("Autofocus is disabled!");
        }

        srchinp.oninput = function() {
            DoSearch(this.value, true);
        }
    }

    if(document.querySelector('#frmsearch')) {
        document.querySelector('#frmsearch').onsubmit = function(e) {
            e.preventDefault();            
        }
    }
}

export { Search }