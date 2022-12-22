function PostSender() {
    if(document.querySelector('#mypostsenderblk')) {
        var userdetails = localStorage.getItem("login") ? JSON.parse(localStorage.getItem("login")) : null;
        var myid = userdetails ?  userdetails.usersId :  0;
        var myimage = userdetails ? userdetails.image : "../assets/images/guest.png";
        var mydisplayname = userdetails ? userdetails.displayname : "Guest";

        myimage = myimage.indexOf('/users') !== -1 ? myimage.replace('/users', '') : myimage;

        document.querySelector('#mypostsenderblk').innerHTML = `
        <div class="blkpostsender">
            <div class="blkpostsenderheader">
                <img src="${myimage}" width="50" height="50" class="img-fluid imguser">
                <form action="" method="post" class="formposter ms-2" id="formposter">
                    <textarea name="text" id="text" cols="1" rows="3" placeholder="What you are thinking this time, ${mydisplayname}?"></textarea>
                    <!-- <input type="text" name="text" id="text" placeholder="What you are thinking this time, ${mydisplayname}?" /> -->
                </form>
            </div>
            <div class="blkpostsenderbody">
                <div class="blkposterbodycontainer mt-3">
                    <div class="input-group t-left d-block">
                        <button class="btn btn-primary btnimages">
                            <i class="bi bi-images"></i>
                            <span class="ms-1">Images</span>
                        </button>
                        <button class="btn btn-primary btnvideos">
                            <i class="bi bi-camera-video"></i>
                            <span class="ms-1">Videos</span>
                        </button>
                        <button class="btn btn-primary btnemojis">
                            <i class="bi bi-emoji-smile"></i>
                            <span class="ms-1">Emojis</span>
                        </button>
                    </div>
                    <div class="input-group t-right d-block">
                        <div class="dropdown selstatus">
                            <button class="btn btn-info dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                            <i class="bi bi-globe me-1"></i>
                            Public
                            </button>
                            <ul class="dropdown-menu">
                            <li>
                                <a class="dropdown-item" href="${location.pathname}?id=${myid}#public">
                                    <i class="bi bi-globe me-1"></i>
                                    Public
                                </a>
                            </li>
                            <li>
                                <a class="dropdown-item" href="${location.pathname}?id=${myid}#private">
                                    <i class="bi bi-lock me-1"></i>
                                    Private
                                </a>
                            </li>
                            </ul>
                        </div>
                        <button type="reset" class="btn btn-secondary btnclear" id="btnclear">
                            <i class="bi bi-x-circle"></i>
                            <span class="ms-1">Clear</span>
                        </button>
                        <button type="button" class="btn btn-primary btnpost" id="btnpost">
                            <i class="bi bi-plus-circle"></i>
                            <span class="ms-1">Post</span>
                        </button>
                    </div>
                </div>
            </div>
        </div>`;
    }
}

function GetFormPostComment() {
    var userdetails = localStorage.getItem("login") ? JSON.parse(localStorage.getItem("login")) : null;
    var myid = userdetails ?  userdetails.usersId :  0;
    var myusername = userdetails ?  userdetails.username :  "guest";

    return `
    <form action="" method="post" class="frmpostercomment" id="frmpostercomment">
        <h4 class="d-block" style="font-size: 1rem; font-weight: bold;">Leave a comment</h4>

        <div class="d-block mt-3 mb-3">
            <label for="txtuname">Username:</label>
            <input type="text" name="txtusername" id="txtusername" class="form-control txtusername mt-3 d-block" placeholder="Write here your username" value="${myusername}" />

            <label for="txtcomment" class="mt-3">Message:</label>
            <textarea name="txtcomment" id="txtcomment" class="form-control txtcomment mt-3 d-block" cols="1" rows="5" placeholder="Just write here your comment to this post!"></textarea>
        </div>

        <div class="input-group mt-3">
            <div class="dropdown selstatus">
                <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                    <i class="bi bi-globe me-1"></i>
                    Public
                </button>
                <ul class="dropdown-menu">
                <li>
                    <a class="dropdown-item" href="pages/profile.html?id=${myid}#public">
                        <i class="bi bi-globe me-1"></i>
                        Public
                    </a>
                </li>
                <li>
                    <a class="dropdown-item" href="pages/profile.html?id=${myid}#private">
                        <i class="bi bi-lock me-1"></i>
                        Private
                    </a>
                </li>
                </ul>
            </div>
            <input type="reset" class="btn btn-secondary btnclearcom ms-2" value="Clear" style="border-radius: 25px" />
            <input type="submit" class="btn btn-primary btnsendcom ms-2" value="Send" style="border-radius: 25px" />
        </div>
    </form>`;
}

function PostsComments() {
    var userdetails = localStorage.getItem("login") ? JSON.parse(localStorage.getItem("login")) : null;
    var myid = userdetails ?  userdetails.usersId :  0;
    var myimage = userdetails ? userdetails.image : "../assets/images/guest.png";
    var mydisplayname = userdetails ? userdetails.displayname : "Guest";

    myimage = myimage.indexOf('/users') !== -1 ? myimage.replace('/users', '') : myimage;

    return `
    <hr>
    <h3>Comments</h3>
    <div class="blkpostercomments mt-3 mb-3">
        ${GetFormPostComment()}
    </div>
    <div class="blkpostcomments mt-3">
        <div class="blkpostcommentsheader">
            <div class="blkphcleft">
                <img src="${myimage}" width="50" height="50" class="img-fluid imguser">
                <div class="blkphctext ms-2">
                    <span class="text-truncate">${mydisplayname}</span>
                    <div class="blkphctextrow">
                        <a href="${location.pathname}?id=${myid}#status"><i class="bi bi-globe"></i> Public</a>
                        <a href="${location.pathname}?id=${myid}#datetime" class="ms-3"><i class="bi bi-calendar-date"></i> 2022-09-02 14:38:00</a>
                    </div>
                </div>
            </div>
            <div class="blkphcright">
                <button class="btn dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                    <i class="bi bi-three-dots"></i>
                </button>
                <ul class="dropdown-menu">
                    <li>
                    <a class="dropdown-item" href="${location.pathname}?id=${myid}#edit">
                        <i class="bi bi-pen"></i>
                        Edit
                    </a>
                    </li>
                    <li>
                    <a class="dropdown-item" href="${location.pathname}?id=${myid}#delete">
                        <i class="bi bi-trash"></i>
                        Delete
                    </a>
                    </li>
                </ul>
            </div>
        </div>
        <div class="blkpostcommentsbody">
            <p class="mb-3">Lorem ipsum dolor sit amet, consectetur adipisicing elit. Nostrum ad aliquam aspernatur quibusdam itaque optio blanditiis, quae, ipsam veniam adipisci ullam? Ducimus obcaecati eaque impedit consequatur necessitatibus laborum, sint natus.</p>
        </div>
    </div>`;
}

function Posts() {
    if(document.querySelector('#mypostsblk')) {
        var userdetails = localStorage.getItem("login") ? JSON.parse(localStorage.getItem("login")) : null;
        var myid = userdetails ?  userdetails.usersId :  0;
        var myimage = userdetails ? userdetails.image : "../assets/images/guest.png";
        var mydisplayname = userdetails ? userdetails.displayname : "Guest";

        myimage = myimage.indexOf('/users') !== -1 ? myimage.replace('/users', '') : myimage;

        document.querySelector('#mypostsblk').innerHTML = `
        <div class="blkpost">
            <div class="blkpostheader">
                <div class="blkphleft">
                    <img src="${myimage}" width="50" height="50" class="img-fluid imguser">
                    <div class="blkphtext ms-2">
                        <span class="text-truncate">${mydisplayname}</span>
                        <div class="blkphtextrow">
                            <a href="${location.pathname}?id=${myid}#status"><i class="bi bi-globe"></i> Public</a>
                            <a href="${location.pathname}?id=${myid}#datetime" class="ms-3"><i class="bi bi-calendar-date"></i> 2022-09-02 14:38:00</a>
                        </div>
                    </div>
                </div>
                <div class="blkphright">
                    <button class="btn dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                        <i class="bi bi-three-dots"></i>
                    </button>
                    <ul class="dropdown-menu">
                        <li>
                        <a class="dropdown-item btneditpost" id="btneditpost" href="pages/post/edit.html?id=${myid}">
                            <i class="bi bi-pen"></i>
                            Edit
                        </a>
                        </li>
                        <li>
                        <a class="dropdown-item btndelpost" id="btndelpost" href="pages/post/delete.html?id=${myid}">
                            <i class="bi bi-trash"></i>
                            Delete
                        </a>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="blkpostbody">
                <img src="/assets/images/default.webp" alt="Default image post" class="imgpost" />
                <p class="mt-3">Lorem ipsum dolor sit amet consectetur, adipisicing elit. Provident amet ea porro tenetur commodi dolor soluta similique. Ratione asperiores tempora assumenda nisi eaque pariatur, sed illum beatae doloribus? Perspiciatis, nostrum?</p>
                <div class="links p-3 d-block">
                    <a href="pages/profile.html?id=${myid}#reacts" id="myreactsstats" class="myreactsstats">
                        0 reacts
                    </a>
                    <a href="pages/profile.html?id=${myid}#shares" id="mysharesstats" class="mysharesstats ms-2">
                        0 shares
                    </a>
                    <a href="pages/profile.html?id=${myid}#comments" id="mycommentsstats" class="mycommentsstats ms-2">
                        0 comments
                    </a>
                </div>
                <div class="actionlinks input-group p-3">
                    <div class="colactionlinks t-left">
                        <div class="blkreactlist hidden" id="blkreactlist">
                            <div class="sreactgrp" id="sreactgrp">
                                <div class="reactgrp reactlike">
                                    <a href="pages/profile.html?id=${myid}#react#like">
                                        <i class="bi bi-hand-thumbs-up-fill"></i>
                                    </a>
                                </div>
                                <div class="reactgrp reactdislike">
                                    <a href="pages/profile.html?id=${myid}#react#dislike">
                                        <i class="bi bi-hand-thumbs-down-fill"></i>
                                    </a>
                                </div>
                                <div class="reactgrp reactsad">
                                    <a href="pages/profile.html?id=${myid}#react#sad">
                                        <i class="bi bi-emoji-frown-fill"></i>
                                    </a>
                                </div>
                                <div class="reactgrp reactangry">
                                    <a href="pages/profile.html?id=${myid}#react#angry">
                                        <i class="bi bi-emoji-angry-fill"></i>
                                    </a>
                                </div>
                                <div class="reactgrp reactlaugh">
                                    <a href="pages/profile.html?id=${myid}#react#laugh">
                                        <i class="bi bi-emoji-laughing-fill"></i>
                                    </a>
                                </div>
                                <div class="reactgrp reactdisgusting">
                                    <a href="pages/profile.html?id=${myid}#react#disgusting">
                                        <i class="bi bi-emoji-dizzy-fill"></i>
                                    </a>
                                </div>
                            </div>
                        </div>
                        <button class="btn btn-primary btnshreact" id="btnshreact">
                            <i class="bi bi-emoji-smile"></i>
                            React
                        </button>
                    </div>
                    <div class="colactionlinks t-center">
                        <button class="btn btn-primary btnshcomments" id="btnshcomments">
                            <i class="bi bi-chat-square-dots"></i>
                            Comment
                        </button>
                    </div>
                    <div class="colactionlinks t-right">
                        <button class="btn btn-primary btnshshares" id="btnshshares">
                            <i class="bi bi-share"></i>
                            Share
                        </button>
                    </div>
                </div>
            </div>
            <div class="blkpostfooter hidden">
                ${PostsComments()}
            </div>
        </div>`;
    }
}

export { PostSender, Posts }