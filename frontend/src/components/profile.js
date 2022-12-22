function ProfileSidebarLeft() {
    if(document.querySelector('#myprofilesbleft')) {
        document.querySelector('#myprofilesbleft').innerHTML = `
            <div class="blkabout">
                <h3 class="title">About</h3>
                <p class="text">Lorem ipsum dolor sit amet consectetur adipisicing elit. Totam alias quidem porro explicabo itaque animi, exercitationem illum quisquam pariatur obcaecati possimus quam numquam harum consectetur. Quam reiciendis voluptatibus suscipit voluptas.</p>
            </div>
            
            <div class="blkfriends">
                <h3 class="title">Friends (2)</h3>
                <div class="table-responsive mt-3">
                    <table class="table table-bordered">
                        <tbody>
                            <tr>
                                <td>
                                    <img src="/assets/images/guest.png" width="50" height="50" alt="John Doe" class="img-fluid imguser guest">
                                    <p class="titleuser mt-3">John Doe</p>
                                </td>
                                <td>
                                    <img src="/assets/images/guest.png" width="50" height="50" alt="Mary Doe" class="img-fluid imguser guest">
                                    <p class="titleuser mt-3">Mary Doe</p>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

            <div class="blkgroups">
                <h3 class="title">Groups (3)</h3>
                <ul class="listgroups">
                    <li class="grp grpgames">
                        <i class="bi bi-controller grpico grpgamesico"></i>
                        <a href="pages/groups.html#games" class="grptitle">Games</a>
                    </li>
                    <li class="grp grpprogramingandit">
                        <i class="bi bi-laptop grpico grpprogitico"></i>
                        <a href="pages/groups.html#progandit" class="grptitle">Programming & IT</a>
                    </li>
                    <li class="grp grpmovies">
                        <i class="bi bi-film grpico grpmoviesico"></i>
                        <a href="pages/groups.html#movies" class="grptitle">Movies</a>
                    </li>
                </ul>
            </div>

            <div class="mblklinksfooter">
                <div class="blklinksfooter">
                    <div class="sublinksfooter">
                        <a href="pages/main.html" class="btn btn-primary btnreturntonfeed" id="btnreturntonfeed">
                            <i class="bi bi-newspaper"></i>
                            <span class="ms-1">Return to my newsfeed</span>
                        </a>
                    </div>
                    <div class="sublinksfooter mt-3">
                        <div class="form-check form-switch">
                            <input class="form-check-input" type="checkbox" role="switch" id="togThemeDarkMode">
                            <label class="form-check-label" for="togThemeDarkMode">Toggle dark mode</label>
                        </div>
                    </div>
                    <div class="sublinksfooter mt-3">
                        <a href="pages/privacy_policy.html">Privacy Policy</a>
                        <a href="pages/about.html">About</a>
                        <a href="pages/terms_of_service.html">Terms of Service</a>
                    </div>
                </div>
            </div>`;
    }
}

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

export { ProfileSidebarLeft, Profile }