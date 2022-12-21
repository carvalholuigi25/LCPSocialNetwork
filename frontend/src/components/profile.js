function Profile() {
    if(document.querySelector('#myprofileblk')) {
        var userdetails = localStorage.getItem("login") ? JSON.parse(localStorage.getItem("login")) : null;
        var myid = userdetails ?  userdetails.usersId :  0;
        var mycover = userdetails ? userdetails.cover : "../assets/images/c_guest.png";
        var myimage = userdetails ? userdetails.image : "../assets/images/guest.png";
        var mydisplayname = userdetails ? userdetails.displayname : "Guest";

        mycover = mycover.indexOf('/users') !== -1 ? mycover.replace('/users', '') : mycover;
        myimage = myimage.indexOf('/users') !== -1 ? myimage.replace('/users', '') : myimage;

        document.querySelector('#myprofileblk').innerHTML = `
            <div class="blkprofile">
                <img src="${mycover}" alt="${mydisplayname}'s Cover" class="imgprofilecover" id="imgprofilecover" />
                <div class="blkprofilebody">
                    <img src="${myimage}" width="50" height="50" class="img-fluid imguser">
                    <div class="blkprofilebodyleft">
                        <span class="username">${mydisplayname}</span>
                    </div>
                    <div class="blkprofilebodyright">
                        <div class="input-group">
                            <a href="pages/profile/edit.html?id=${myid}" class="btn btn-warning btneditprofile">
                                <i class="bi bi-pen"></i>
                                <span class="ms-1">Edit profile</span>
                            </a>
                            <a href="pages/profile/report.html?id=${myid}" class="btn btn-error btnreportuser">
                                <i class="bi bi-exclamation-circle-fill"></i>
                                <span class="ms-1">Report this user</span>
                            </a>
                        </div>
                    </div>
                </div>
                <div class="blkprofilefooter">
                    <ul class="nav nav-tabs navprofileshlinks">
                        <li class="nav-item">
                            <a class="nav-link active" aria-current="page" href="pages/profile.html?id=${myid}#posts">Posts</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="pages/profile.html?id=${myid}#images">Images</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="pages/profile.html?id=${myid}#videos">Videos</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="pages/profile.html?id=${myid}#about">About</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="pages/profile.html?id=${myid}#events">Events</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="pages/profile.html?id=${myid}#activity">Activity</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="pages/profile.html?id=${myid}#friends">Friends</a>
                        </li>
                    </ul>
                </div>
            </div>`;
    }
}

export { Profile }