import { fetchingData, fetchPostsAndUsers, getMyApiUrl } from "../scripts/my_functions";
import { doActionBtnModals, getMyQueryVal } from "./geral";
import { GetNotifications } from './notifications';
import { Create, Read } from "./crud";

function PostSender() {
    if(document.querySelector('#mypostsenderblk')) {
        var mycover = ""; var myimage = ""; var mydisplayname = "";
        var userdetails = localStorage.getItem("login") ? JSON.parse(localStorage.getItem("login")) : null;
        var myulid = userdetails ?  userdetails.usersId :  0;
        var myid = getMyQueryVal().id ? getMyQueryVal().id : myulid;
        var apiUrl = getMyApiUrl();

        fetchingData(`${apiUrl}/api/users/${myid}`, "GET", null, userdetails.token, true).then(([users]) => {
            if(users.data != null && users.length > 0) {
                var usersres = users.data != null && users.length > 0 ? JSON.parse(JSON.stringify(users)).data : null;
                mycover = usersres ? usersres.cover : "../assets/images/c_guest.png";
                myimage = usersres ? usersres.image : "../assets/images/guest.png";
                mydisplayname = usersres ? usersres.displayname : "Guest";
        
                mycover = mycover.indexOf('/users') !== -1 ? mycover.replace('/users', '') : mycover;
                myimage = myimage.indexOf('/users') !== -1 ? myimage.replace('/users', '') : myimage;

                if(myulid == usersres.id) {
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

                    if(document.querySelector('.blkpostsender #formposter')) {
                        document.querySelector('.blkpostsender #formposter').onsubmit = function(e) {
                            e.preventDefault();
                        };
                    }

                    if(document.querySelector('.blkpostsender #btnpost')) {
                        document.querySelector('.blkpostsender #btnpost').onclick = function(e) {
                            e.preventDefault();
                            var data = {
                                title: "Post from " + mydisplayname,
                                shortdesc: document.querySelector('#formposter #text').value,
                                text: document.querySelector('#formposter #text').value,
                                image: mycover,
                                attachments: null,
                                status: "published",
                                privacy: "publictxt",
                                isFeatured: false,
                                dateCreated: new Date().toISOString(),
                                dateModified: null,
                                dateDeleted: null,
                                usersId: myid,
                                reactsId: 1
                            };

                            Create("posts", data, userdetails.token).then(x => {
                                GetNotifications("blksubnotifications", "text-bg-success", "bi-check-lg", "Created new post successfully!", true, 1000);
                                setTimeout(() => {
                                    location.href = "pages/main.html";
                                }, 1000);
                            }).catch(err => {
                                console.log(err);
                                GetNotifications("blksubnotifications", "text-bg-error", "bi-x", "Error: Could not to create a new post!", true, 1000);
                            });
                        };
                    }
    
                    doActionBtnModals();
                }
            }
        });
    }
}

function GetFormPostComment(myid, myusername) {
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

function PostsComments(myid, mydisplayname, myimage) {
    return `
    <hr>
    <h3>Comments</h3>
    <div class="blkpostercomments blkpostercomments${myid} mt-3 mb-3">
        ${GetFormPostComment(myid, mydisplayname)}
    </div>
    <div class="blkpostcomments blkpostcomments${myid} mt-3">
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
        var mycover = ""; var myimage = ""; var mydisplayname = "";
        var userdetails = localStorage.getItem("login") ? JSON.parse(localStorage.getItem("login")) : null;
        var myid = getMyQueryVal().id ? getMyQueryVal().id : (userdetails ? userdetails.id : -1);
        var gmyid = myid ? myid : -1;
        var apiUrl = getMyApiUrl();

        fetchPostsAndUsers(-1, gmyid, userdetails.token, null).then(([posts, users]) => {
            users = users.data != null ? [users.data] : users;
            if(posts.length > 0 && users.length > 0) {
               posts = posts.sort((a, b) => -a.dateCreated.localeCompare(b.dateCreated));
               posts = getMyQueryVal().postid ? posts.filter(x => x.postId == getMyQueryVal().postid) : posts;
               
               users.forEach(elmusers => {
                posts.forEach(elmposts => {
                    if(elmposts != null && elmposts.usersId == elmusers.id) {
                        var postsactions = "";
                        mycover = elmusers.cover != null ? elmusers.cover : "../assets/images/c_guest.png";
                        myimage = elmusers.image != null ? elmusers.image : "../assets/images/guest.png";
                        mydisplayname = elmusers.displayname != null ? elmusers.displayname : "Guest";

                        mycover = mycover.indexOf('/users') !== -1 ? mycover.replace('/users', '') : mycover;
                        myimage = myimage.indexOf('/users') !== -1 ? myimage.replace('/users', '') : myimage;
    
                        if(["superadmin", "admin", "moderator", "user", "editor"].includes(userdetails.role)) {
                            postsactions = `
                            <button class="btn dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                                <i class="bi bi-three-dots"></i>
                            </button>
                            <ul class="dropdown-menu">
                                <li>
                                    <a class="dropdown-item btneditpost" id="btneditpost" href="pages/post/edit.html?id=${elmposts.postId}&userid=${elmposts.usersId}">
                                        <i class="bi bi-pen"></i>
                                        Edit
                                    </a>
                                </li>
                                <li>
                                    <a class="dropdown-item btndelpost" id="btndelpost" href="pages/post/delete.html?id=${elmposts.postId}&userid=${elmposts.usersId}">
                                        <i class="bi bi-trash"></i>
                                        Delete
                                    </a>
                                </li>
                            </ul>`;
                        }
    
                        document.querySelector('#mypostsblk').innerHTML += `
                        <div class="blkpost mt-3" data-id="${elmposts.postId}">
                            <div class="blkpostheader">
                                <div class="blkphleft">
                                    <a href="pages/profile.html?id=${elmposts.usersId}">
                                        <img src="${myimage}" width="50" height="50" class="img-fluid imguser" />
                                    </a>
                                    <div class="blkphtext ms-2">
                                        <span class="text-truncate"><a href="pages/profile.html?id=${elmposts.usersId}">${mydisplayname}</a></span>
                                        <div class="blkphtextrow">
                                            <a href="${location.pathname}?id=${elmposts.usersId}&postid=${elmposts.postId}#status"><i class="bi bi-globe"></i> ${elmposts.status}</a>
                                            <a href="${location.pathname}?id=${elmposts.usersId}&postid=${elmposts.postId}#datetime" class="ms-3"><i class="bi bi-calendar-date"></i> ${elmposts.dateCreated}</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="blkphright">
                                    ${postsactions}
                                </div>
                            </div>
                            <div class="blkpostbody">
                                <img src="${elmposts.image}" alt="Post image from usersid ${elmposts.usersId}" class="imgpost" />
                                <p class="mt-3">${elmposts.text}</p>
                                <div class="links p-3 d-block">
                                    <a href="pages/profile.html?id=${elmposts.usersId}&postid=${elmposts.postId}#reacts" id="myreactsstats" class="myreactsstats" data-id="${elmposts.postId}">
                                        0 reacts
                                    </a>
                                    <a href="pages/profile.html?id=${elmposts.usersId}&postid=${elmposts.postId}#shares" id="mysharesstats" class="mysharesstats ms-2" data-id="${elmposts.postId}">
                                        0 shares
                                    </a>
                                    <a href="pages/profile.html?id=${elmposts.usersId}&postid=${elmposts.postId}#comments" id="mycommentsstats" class="mycommentsstats ms-2" data-id="${elmposts.postId}">
                                        0 comments
                                    </a>
                                </div>
                                <div class="actionlinks input-group p-3">
                                    <div class="colactionlinks t-left">
                                        <div class="blkreactlist blkreactlist${elmposts.postId} hidden" id="blkreactlist">
                                            <div class="sreactgrp" id="sreactgrp">
                                                <div class="reactgrp reactlike">
                                                    <a href="pages/profile.html?id=${elmposts.usersId}#react#like" data-id="${elmposts.postId}">
                                                        <i class="bi bi-hand-thumbs-up-fill"></i>
                                                    </a>
                                                </div>
                                                <div class="reactgrp reactdislike">
                                                    <a href="pages/profile.html?id=${elmposts.usersId}#react#dislike" data-id="${elmposts.postId}">
                                                        <i class="bi bi-hand-thumbs-down-fill"></i>
                                                    </a>
                                                </div>
                                                <div class="reactgrp reactsad">
                                                    <a href="pages/profile.html?id=${elmposts.usersId}#react#sad" data-id="${elmposts.postId}">
                                                        <i class="bi bi-emoji-frown-fill"></i>
                                                    </a>
                                                </div>
                                                <div class="reactgrp reactangry">
                                                    <a href="pages/profile.html?id=${elmposts.usersId}#react#angry" data-id="${elmposts.postId}">
                                                        <i class="bi bi-emoji-angry-fill"></i>
                                                    </a>
                                                </div>
                                                <div class="reactgrp reactlaugh">
                                                    <a href="pages/profile.html?id=${elmposts.usersId}#react#laugh" data-id="${elmposts.postId}">
                                                        <i class="bi bi-emoji-laughing-fill"></i>
                                                    </a>
                                                </div>
                                                <div class="reactgrp reactdisgusting">
                                                    <a href="pages/profile.html?id=${elmposts.usersId}#react#disgusting" data-id="${elmposts.postId}">
                                                        <i class="bi bi-emoji-dizzy-fill"></i>
                                                    </a>
                                                </div>
                                            </div>
                                        </div>
                                        <button class="btn btn-primary btnshreact" id="btnshreact" data-id="${elmposts.postId}">
                                            <i class="bi bi-emoji-smile"></i>
                                            React
                                        </button>
                                    </div>
                                    <div class="colactionlinks t-center">
                                        <button class="btn btn-primary btnshcomments" id="btnshcomments" data-id="${elmposts.postId}">
                                            <i class="bi bi-chat-square-dots"></i>
                                            Comment
                                        </button>
                                    </div>
                                    <div class="colactionlinks t-right">
                                        <button class="btn btn-primary btnshshares" id="btnshshares" data-id="${elmposts.postId}">
                                            <i class="bi bi-share"></i>
                                            Share
                                        </button>
                                    </div>
                                </div>
                            </div>
                            <div class="blkpostfooter blkpostfooter${elmposts.postId} hidden">
                                ${PostsComments(myid, mydisplayname, myimage)}
                            </div>
                        </div>`;
    
                        doActionBtnModals();
                    }
                });
               });
            } else {
                document.querySelector('#mypostsblk').innerHTML = `
                    <div class="warnblk">
                        <i class="bi bi-exclamation-circle" style="font-size: 4rem; color: red;"></i>
                        <h3>No posts has been found for this user!</h3>
                    </div>
                `;
            }
        });
    }
}

export { PostSender, Posts }