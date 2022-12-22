import '../scss/dep_styles.scss';
import * as bootstrap from 'bootstrap';
import * as comp from '../components/index';

comp.Navbar();
comp.Sidebars();
comp.Footer();
comp.ProfileSidebarLeft();
comp.toggleTheme();
comp.Profile();
comp.Friends();
comp.Stories();
comp.PostSender();
comp.Posts();
comp.MyFriendsRequests();
comp.MyPrivateMessages();
comp.MyNotifications();
comp.MyReactions();
comp.MyShares();
comp.MySharePost();
comp.EditMyPost();
comp.DelMyPost();

var isfixednav = false;

if(isfixednav && !document.body.classList.contains('fixednav')) {
    document.body.classList.add("fixednav");
} else {
    document.body.classList.remove("fixednav");
}

if(document.querySelector('#btnclear')) {
    document.querySelector('#btnclear').onclick = function(e) {
        e.preventDefault();
        document.querySelector('#formposter').reset();
    };
}

if(document.querySelector('#btneditpost')) {
    document.querySelector('#btneditpost').onclick = function(e) {
        e.preventDefault();
        const modaleditmypost = new bootstrap.Modal('#modaleditmypost');
        modaleditmypost.show();
    };
}

if(document.querySelector('#btndelpost')) {
    document.querySelector('#btndelpost').onclick = function(e) {
        e.preventDefault();
        const modaldelmypost = new bootstrap.Modal('#modaldelmypost');
        modaldelmypost.show();
    };
}

if(document.querySelector('#myreactsstats')) {
    document.querySelector('#myreactsstats').onclick = function(e) {
        e.preventDefault();
        const modalmyreactions = new bootstrap.Modal('#modalmyreactions');
        modalmyreactions.show();
    };
}

if(document.querySelector('#mysharesstats')) {
    document.querySelector('#mysharesstats').onclick = function(e) {
        e.preventDefault();
        const modalmyshares = new bootstrap.Modal('#modalmyshares');
        modalmyshares.show();
    };
}

if(document.querySelector('#btnshreact')) {
    document.querySelector('#btnshreact').onclick = function() {
        if(document.querySelector('#blkreactlist').classList.contains('hidden')) {
            document.querySelector('#blkreactlist').classList.remove('hidden');
        } else {
            document.querySelector('#blkreactlist').classList.add('hidden');
        }
    };

    // document.querySelector('#btnshreact').onmouseover = function() {
    //     if(document.querySelector('#blkreactlist').classList.contains('hidden')) {
    //         document.querySelector('#blkreactlist').classList.remove('hidden');
    //     }
    // };

    // document.querySelector('#btnshreact').onmouseout = function() {
    //     if(!document.querySelector('#blkreactlist').classList.contains('hidden')) {
    //         document.querySelector('#blkreactlist').classList.add('hidden');
    //     }
    // };
}

if(document.querySelector('#btnshcomments')) {
    if(document.querySelectorAll('.blkpostfooter')[0]) {
        if(!document.querySelectorAll('.blkpostfooter')[0].classList.contains('hidden')) {
            localStorage.setItem('showComments', true);
        }
    
        if(localStorage.getItem('showComments') == "true") {
            document.querySelectorAll('.blkpostfooter')[0].classList.remove('hidden');
        }
    }

    document.querySelector('#btnshcomments').onclick = function() {
        if(document.querySelectorAll('.blkpostfooter')[0].classList.contains('hidden')) {
            localStorage.setItem('showComments', true);
            document.querySelectorAll('.blkpostfooter')[0].classList.remove('hidden');
        } else {
            localStorage.setItem('showComments', false);
            document.querySelectorAll('.blkpostfooter')[0].classList.add('hidden');
        }
    };
}

if(document.querySelector('#btnshshares')) {
    document.querySelector('#btnshshares').onclick = function(e) {
        e.preventDefault();
        const modalmysharepost = new bootstrap.Modal('#modalmysharepost');
        modalmysharepost.show();
    };
}