function Friends() {
    if(document.querySelector('#myfriendslistblk')) {
        document.querySelector('#myfriendslistblk').innerHTML = `
        <div class="col-12 col-md-12 col-lg-6 col-xl-4 col-xxl-4">
            <div class="blklistfriends">
                <div class="blklistfriendsheader">
                    <a href="pages/profile.html?id=2#edit" class="editprofilelistfriends">
                        <i class="bi bi-pen"></i>
                    </a>
                    <img src="assets/images/default.webp" alt="Luigi Carvalho Cover" class="coverlistfriends">
                    <img src="assets/images/luigi.png" alt="Luigi Carvalho Avatar" class="imglistfriends">
                </div>
                <div class="blklistfriendsbody">
                    <h3 class="name">Luigi Carvalho</h3>
                    <div class="blklistfriendsbodymcol">
                        <div class="blklistfriendsbodycol1">
                            <span>Friend(s): 1</span>
                        </div>
                        <div class="blklistfriendsbodycol2">
                            <span>Role: User</span>
                        </div>
                    </div>
                    <div class="input-group">
                        <button class="col-12 col-md-12 btn btn-primary btnsendpm" id="btnsendpm">Send PM</button>
                        <button class="col-12 col-md-12 btn btn-primary btnrepblock ms-1" id="btnrepblock">Report / Block</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-12 col-md-12 col-lg-6 col-xl-4 col-xxl-4">
            <div class="blklistfriends">
                <div class="blklistfriendsheader">
                    <a href="pages/profile.html?id=3#edit" class="editprofilelistfriends">
                        <i class="bi bi-pen"></i>
                    </a>
                    <img src="assets/images/default.webp" alt="Guest Cover" class="coverlistfriends">
                    <img src="assets/images/guest.png" alt="Guest Avatar" class="imglistfriends guest">
                </div>
                <div class="blklistfriendsbody">
                    <h3 class="name">Guest Convidado</h3>
                    <div class="blklistfriendsbodymcol">
                        <div class="blklistfriendsbodycol1">
                            <span>Friend(s): 1</span>
                        </div>
                        <div class="blklistfriendsbodycol2">
                            <span>Role: User</span>
                        </div>
                    </div>
                    <div class="input-group">
                        <button class="col-12 btn btn-primary btnsendpm" id="btnsendpm">Send PM</button>
                        <button class="col-12 btn btn-primary btnrepblock ms-1" id="btnrepblock">Report / Block</button>
                    </div>
                </div>
            </div>
        </div>`;
    }
}

export { Friends }