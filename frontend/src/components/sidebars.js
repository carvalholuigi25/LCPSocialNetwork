function Sidebars() {
    if(document.querySelector('#mysidebarleft')) {
        var userdetails = localStorage.getItem("login");
        var jspdata = JSON.parse(userdetails);
        var myid = userdetails && jspdata != null ?  jspdata.usersId :  0;
        var mycover = userdetails && jspdata != null ? jspdata.cover :  "../assets/images/c_guest.png";
        var myimage = userdetails && jspdata != null ? jspdata.image :  "../assets/images/guest.png";
        var mydisplayname = userdetails && jspdata != null ? jspdata.displayname : "Guest";

        mycover = mycover.indexOf('/users') !== -1 ? mycover.replace('/users', '') : mycover;
        myimage = myimage.indexOf('/users') !== -1 ? myimage.replace('/users', '') : myimage;
        
        document.querySelector('#mysidebarleft').innerHTML = `
        <div class="blkavatar">
            <a href="pages/profile.html?id=${myid}">
                <img src="${mycover}" width="1200" height="300" loading="lazy" class="img-fluid avatarcover br-half-sqr" />
                <img src="${myimage}" width="40" height="40" loading="lazy" class="img-fluid avatarimg br-full-sqr" />
            </a>
        </div>
        <h5 class="blkuser">${mydisplayname}</h5>
        <div class="blklinks">
            <div class="row">
                <div class="col-12">
                    <a href="pages/friends.html" class="btn btn-primary">
                        <i class="bi bi-people-fill me-1"></i>
                        <span>Friends</span>
                    </a>
                </div>
                <div class="col-12">
                    <a href="pages/groups.html" class="btn btn-primary mt-3">
                        <i class="bi bi-people-fill me-1"></i>
                        <span>Groups</span>
                    </a>
                </div>
                <div class="col-12 col-md-6">
                    <a href="pages/userthings/animes.html" class="btn btn-primary mt-3">
                        <i class="bi bi-people-fill me-1"></i>
                        <span>Animes</span>
                    </a>
                </div>
                <div class="col-12 col-md-6">
                    <a href="pages/userthings/comicbooks.html" class="btn btn-primary mt-3">
                        <i class="bi bi-people-fill me-1"></i>
                        <span>Comic Books</span>
                    </a>
                </div>
                <div class="col-12 col-md-6">
                    <a href="pages/userthings/games.html" class="btn btn-primary mt-3">
                        <i class="bi bi-people-fill me-1"></i>
                        <span>Games</span>
                    </a>
                </div>
                <div class="col-12 col-md-6">
                    <a href="pages/userthings/mangas.html" class="btn btn-primary mt-3">
                        <i class="bi bi-people-fill me-1"></i>
                        <span>Mangas</span>
                    </a>
                </div>
                <div class="col-12 col-md-6">
                    <a href="pages/userthings/movies.html" class="btn btn-primary mt-3">
                        <i class="bi bi-people-fill me-1"></i>
                        <span>Movies</span>
                    </a>
                </div>
                <div class="col-12 col-md-6">
                    <a href="pages/userthings/tvseries.html" class="btn btn-primary mt-3">
                        <i class="bi bi-people-fill me-1"></i>
                        <span>TV Series</span>
                    </a>
                </div>
            </div>
        </div>
        <div class="mblklinksfooter">
            <div class="blklinksfooter">
                <div class="sublinksfooter">
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

export { Sidebars }