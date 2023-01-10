import * as bootstrap from 'bootstrap';
import { getMyApiUrl, fetchingData } from '../scripts/my_functions';

function getMyQueryVal() {
    return new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });
}

function setFixedNavbar() {
    var isfixednav = false;

    if(isfixednav && !document.body.classList.contains('fixednav')) {
        document.body.classList.add("fixednav");
    } else {
        document.body.classList.remove("fixednav");
    }
}

function getMyCurId() {
    var userdetails = localStorage.getItem("login") ? JSON.parse(localStorage.getItem("login")) : null;
    var myulid = userdetails ?  userdetails.usersId :  0;
    var myid = getMyQueryVal().id ? getMyQueryVal().id : myulid;
    return myid;
}

function getMyCurToken() {
    var userdetails = localStorage.getItem("login") ? JSON.parse(localStorage.getItem("login")) : null;
    return userdetails ? userdetails.token : null;
}

function getLength(apiname, id = -1) {
    var userdetails = localStorage.getItem("login") ? JSON.parse(localStorage.getItem("login")) : null;
    var apiUrl = getMyApiUrl();
    var myid = id != -1 ? `/${id}` : "";

    fetchingData(`${apiUrl}/api/${apiname}/count${myid}`, "GET", userdetails.token, true).then(x => {
        localStorage.setItem("len", x.length);
    }).catch(err => { localStorage.setItem("len", 0); });

    return localStorage.getItem("len");
}

function doActionBtnModals() {
    var myid = getMyQueryVal().postid ? getMyQueryVal().postid : -1;
    var len = getLength("posts", myid);

    if(document.querySelector('#btnclear')) {
        document.querySelector('#btnclear').onclick = function(e) {
            e.preventDefault();
            document.querySelector('#formposter').reset();
        };
    }
    
    if(len > 0) {
        for(var i = 0; i < len; i++) {
            if(document.querySelectorAll('#myreactsstats')[i]) {
                document.querySelectorAll('#myreactsstats')[i].onclick = function(e) {
                    e.preventDefault();
                    const modalmyreactions = new bootstrap.Modal('#modalmyreactions'+this.getAttribute('data-id'));
                    modalmyreactions.show();
                };
            }
    
            if(document.querySelectorAll('#mysharesstats')[i]) {
                document.querySelectorAll('#mysharesstats')[i].onclick = function(e) {
                    e.preventDefault();
                    const modalmyshares = new bootstrap.Modal('#modalmyshares'+this.getAttribute('data-id'));
                    modalmyshares.show();
                };
            }
    
            if(document.querySelectorAll('#btnshshares')[i]) {
                document.querySelectorAll('#btnshshares')[i].onclick = function(e) {
                    e.preventDefault();
                    const modalmysharepost = new bootstrap.Modal('#modalmysharepost'+this.getAttribute('data-id'));
                    modalmysharepost.show();
                };
            }
    
            if(document.querySelectorAll('#btnshreact')[i]) {
                document.querySelectorAll('#btnshreact')[i].onclick = function() {
                    if(document.querySelectorAll('.blkreactlist'+this.getAttribute('data-id'))[0].classList.contains('hidden')) {
                        document.querySelectorAll('.blkreactlist'+this.getAttribute('data-id'))[0].classList.remove('hidden');
                    } else {
                        document.querySelectorAll('.blkreactlist'+this.getAttribute('data-id'))[0].classList.add('hidden');
                    }
                };
            }
    
            if(document.querySelectorAll('#btnshcomments')[i]) {
                if(document.querySelectorAll('.blkpostfooter')[i]) {
                    if(!document.querySelectorAll('.blkpostfooter')[i].classList.contains('hidden')) {
                        localStorage.setItem('showComments', true);
                    }
                
                    if(localStorage.getItem('showComments') == "true") {
                        document.querySelectorAll('.blkpostfooter')[i].classList.remove('hidden');
                    }
                }
            
                document.querySelectorAll('#btnshcomments')[i].onclick = function() {
                    if(document.querySelectorAll('.blkpostfooter'+this.getAttribute('data-id'))[0].classList.contains('hidden')) {
                        localStorage.setItem('showComments', true);
                        document.querySelectorAll('.blkpostfooter'+this.getAttribute('data-id'))[0].classList.remove('hidden');
                    } else {
                        localStorage.setItem('showComments', false);
                        document.querySelectorAll('.blkpostfooter'+this.getAttribute('data-id'))[0].classList.add('hidden');
                    }
                };
            }
        }
    }
}

export { getMyQueryVal, setFixedNavbar, getMyCurId, getMyCurToken, getLength, doActionBtnModals }