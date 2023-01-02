import { Users } from "../classes";
import { getMyApiUrl, fetchingData } from "../scripts/my_functions";

function DoSearch(srchval) {
    const apiUrl = getMyApiUrl();
    var mylogin = localStorage.getItem('login') ? JSON.parse(localStorage.getItem('login')) : null;
    var usersreslist = document.querySelector('#usersreslist');

    if(usersreslist) {
        if(srchval.length > 0) {
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
                    x.username.includes(srchval.toLowerCase() || 
                    x.email.includes(srchval.toLowerCase()) || 
                    x.displayname.includes(srchval.toLowerCase()))
                ) : users;
                var usersres = JSON.parse(JSON.stringify(musers));

                if(usersres != null) {
                    var uid = usersres[0].id ?? 0;
                    var uname = usersres[0].firstName != null && usersres[0].lastName != null ? `${usersres[0].firstName}  ${usersres[0].lastName}`: `${usersres[0].username}`;
                    var uavatar = usersres[0].image.indexOf("/users") !== -1 ? usersres[0].image.replace("/users", "") : usersres[0].image;

                    usersreslist.innerHTML = `
                    <a href="pages/profile.html?id=${uid}" target="_blank">
                        <img src="${uavatar}"  class="img-avatar" width="50" height="50" loading="lazy" />
                        <span class="userfullname">${uname}</span>
                    </a>`;
                } else {
                    usersreslist.innerHTML = `<span>No users has been found with this search!</span>`;
                }
            }).catch(err => console.log("Failed to search: " + err));
        }, 500);
    }
}

function Search() {
    var srchinp = document.querySelector('#inpsearch');

    if(srchinp) {
        srchinp.oninput = function() {
            DoSearch(this.value);
        }
    }

    if(document.querySelector('#frmsearch')) {
        document.querySelector('#frmsearch').onsubmit = function(e) {
            e.preventDefault();            
        }
    }
}

export { Search }