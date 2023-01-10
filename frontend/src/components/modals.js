import { Read } from './crud';
import { getMyCurId, getLength, getMyCurToken, getMyQueryVal } from './geral';

function MyFriendsRequests() {
    if(document.querySelector('#blkmodalmyfriendreqs')) {
        document.querySelector('#blkmodalmyfriendreqs').innerHTML = `
            <div class="modalmyfriendreqs modal fade" id="modalmyfriendreqs" tabindex="-1" aria-labelledby="modalmyfriendreqslbl" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                    <div class="modal-content">
                        <div class="modal-header bg-primary">
                            <h5 class="modal-title" id="modalmyfriendreqslbl">
                                <i class="bi bi-people-fill"></i>
                                Friends Requests
                            </h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <p>0 friends requests!</p>
                        </div>
                        <div class="modal-footer bg-primary">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        `;
    }
}

function MyPrivateMessages() {
    if(document.querySelector('#blkmodalmyprivatemessages')) {
        document.querySelector('#blkmodalmyprivatemessages').innerHTML = `
            <div class="modalmyprivatemessages modal fade" id="modalmyprivatemessages" tabindex="-1" aria-labelledby="modalmyprivatemessageslbl" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                    <div class="modal-content">
                        <div class="modal-header bg-primary">
                            <h5 class="modal-title" id="modalmyprivatemessageslbl">
                                <i class="bi bi-chat-fill"></i>
                                My private messages
                            </h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <p>0 private messages!</p>
                        </div>
                        <div class="modal-footer bg-primary">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        `;
    }
}

function MyNotifications() {
    if(document.querySelector('#blkmodalmynotifications')) {
        document.querySelector('#blkmodalmynotifications').innerHTML = `
            <div class="modalmynotifications modal fade" id="modalmynotifications" tabindex="-1" aria-labelledby="modalmynotificationslbl" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                    <div class="modal-content">
                        <div class="modal-header bg-primary">
                            <h5 class="modal-title" id="modalmynotificationslbl">
                                <i class="bi bi-bell-fill"></i>
                                My notifications
                            </h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <p>0 notifications!</p>
                        </div>
                        <div class="modal-footer bg-primary">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        `;
    }
}

function MyShares() {
    if(document.querySelector('#blkmodalmyshares')) {
        var myid = getMyQueryVal().postid ? getMyQueryVal().postid : -1;
        var ary = [];
        var rx = "";

        Read("posts", myid, getMyCurToken()).then(x => {
            if(typeof x === "object") {
                ary.push(x);
                rx = ary[0];
            } else {
                rx = x;
            }

            if(rx != null && rx.length > 0) {
                rx.forEach(elmp => {
                    document.querySelector('#blkmodalmyshares').innerHTML += `
                        <div class="modalmyshares modalmyshares${elmp.postId} modal fade" id="modalmyshares${elmp.postId}" data-id="${elmp.postId}" tabindex="-1" aria-labelledby="modalmyshareslbl" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                                <div class="modal-content">
                                    <div class="modal-header bg-primary">
                                        <h5 class="modal-title" id="modalmyshareslbl">
                                            <i class="bi bi-share"></i>
                                            My shares (id: ${elmp.postId})
                                        </h5>
                                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                    </div>
                                    <div class="modal-body">
                                        <p>0 shares!</p>
                                    </div>
                                    <div class="modal-footer bg-primary">
                                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    `;
                });
            }
        }).catch(err => console.log(err));
    }
}

function MyReactions() {
    if(document.querySelector('#blkmodalmyreactions')) {
        var myid = getMyQueryVal().postid ? getMyQueryVal().postid : -1;
        var ary = [];
        var rx = "";

        Read("posts", myid, getMyCurToken()).then(x => {
           if(typeof x === "object") {
                ary.push(x);
                rx = ary[0];
            } else {
                rx = x;
            }

            if(rx != null && rx.length > 0) {
                rx.forEach(elmp => {
                    document.querySelector('#blkmodalmyreactions').innerHTML += `
                        <div class="modalmyreactions modalmyreactions${elmp.postId} modal fade" id="modalmyreactions${elmp.postId}" data-id="${elmp.postId}" tabindex="-1" aria-labelledby="modalmyreactionslbl" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                                <div class="modal-content">
                                    <div class="modal-header bg-primary">
                                        <h5 class="modal-title" id="modalmyreactionslbl">
                                            <i class="bi bi-emoji-smile"></i>
                                            My reactions (id: ${elmp.postId})
                                        </h5>
                                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                    </div>
                                    <div class="modal-body">
                                        <p>0 reactions!</p>
                                    </div>
                                    <div class="modal-footer bg-primary">
                                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    `;
                });
            }
        }).catch(err => console.log(err));
    }
}

function MySharePost() {
    if(document.querySelector('#blkmodalmysharepost')) {
        var myid = getMyQueryVal().postid ? getMyQueryVal().postid : -1;
        var ary = [];
        var rx = "";

        Read("posts", myid, getMyCurToken()).then(x => {
           if(typeof x === "object") {
                ary.push(x);
                rx = ary[0];
            } else {
                rx = x;
            }
            
            if(rx != null && rx.length > 0) {
                rx.forEach(elmp => {
                    document.querySelector('#blkmodalmysharepost').innerHTML += `
                    <div class="modalmysharepost modalmysharepost${elmp.postId} modal fade" id="modalmysharepost${elmp.postId}" data-id="${elmp.postId}" tabindex="-1" aria-labelledby="modalmysharepostlbl" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                            <div class="modal-content">
                                <div class="modal-header bg-primary">
                                    <h5 class="modal-title" id="modalmysharepostlbl">
                                        <i class="bi bi-share"></i>
                                        Share this post (id: ${elmp.postId})
                                    </h5>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>
                                <div class="modal-body">
                                    <p>Do you want share this post?</p>
                                </div>
                                <div class="modal-footer bg-primary">
                                    <button type="button" class="btn btn-info" data-bs-dismiss="modal">Yes</button>
                                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">No</button>
                                </div>
                            </div>
                        </div>
                    </div>`;
                });
            }
        }).catch(err => console.log(err));
    }
}

export { MyFriendsRequests, MyPrivateMessages, MyNotifications, MyShares, MyReactions, MySharePost }