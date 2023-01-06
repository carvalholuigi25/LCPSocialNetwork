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
        document.querySelector('#blkmodalmyshares').innerHTML = `
            <div class="modalmyshares modal fade" id="modalmyshares" tabindex="-1" aria-labelledby="modalmyshareslbl" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                    <div class="modal-content">
                        <div class="modal-header bg-primary">
                            <h5 class="modal-title" id="modalmyshareslbl">
                                <i class="bi bi-share"></i>
                                My shares
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
    }
}

function MyReactions() {
    if(document.querySelector('#blkmodalmyreactions')) {
        document.querySelector('#blkmodalmyreactions').innerHTML = `
            <div class="modalmyreactions modal fade" id="modalmyreactions" tabindex="-1" aria-labelledby="modalmyreactionslbl" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                    <div class="modal-content">
                        <div class="modal-header bg-primary">
                            <h5 class="modal-title" id="modalmyreactionslbl">
                                <i class="bi bi-emoji-smile"></i>
                                My reactions
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
    }
}

function MySharePost() {
    if(document.querySelector('#blkmodalmysharepost')) {
        document.querySelector('#blkmodalmysharepost').innerHTML = `
            <div class="modalmysharepost modal fade" id="modalmysharepost" tabindex="-1" aria-labelledby="modalmysharepostlbl" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                    <div class="modal-content">
                        <div class="modal-header bg-primary">
                            <h5 class="modal-title" id="modalmysharepostlbl">
                                <i class="bi bi-share"></i>
                                Share this post
                            </h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <p>Do you want share this post (id: 1)?</p>
                        </div>
                        <div class="modal-footer bg-primary">
                            <button type="button" class="btn btn-info" data-bs-dismiss="modal">Yes</button>
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">No</button>
                        </div>
                    </div>
                </div>
            </div>
        `;
    }
}

function EditMyPost(id = 1) {
    if(document.querySelector('#blkmodaleditmypost')) {
        document.querySelector('#blkmodaleditmypost').innerHTML = `
            <div class="modaleditmypost modal fade" id="modaleditmypost" tabindex="-1" aria-labelledby="modaleditmypostlbl" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                    <div class="modal-content">
                        <div class="modal-header bg-info">
                            <h5 class="modal-title" id="modaleditmypostlbl">
                                <i class="bi bi-pen"></i>
                                Edit this post
                            </h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <p>Do you want edit this post (id: ${id})?</p>
                        </div>
                        <div class="modal-footer bg-info">
                            <button type="button" class="btn btn-info" data-bs-dismiss="modal">Yes</button>
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">No</button>
                        </div>
                    </div>
                </div>
            </div>
        `;
    }
}

function DelMyPost(id = 1) {
    if(document.querySelector('#blkmodaldelmypost')) {
        document.querySelector('#blkmodaldelmypost').innerHTML = `
            <div class="modaldelmypost modal fade" id="modaldelmypost" tabindex="-1" aria-labelledby="modaldelmypostlbl" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                    <div class="modal-content">
                        <div class="modal-header bg-warning">
                            <h5 class="modal-title" id="modaldelmypostlbl">
                                <i class="bi bi-trash"></i>
                                Delete this post
                            </h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <p>Do you want delete this post (id: ${id})?</p>
                        </div>
                        <div class="modal-footer bg-warning">
                            <button type="button" class="btn btn-info" data-bs-dismiss="modal">Yes</button>
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">No</button>
                        </div>
                    </div>
                </div>
            </div>
        `;
    }
}

export { MyFriendsRequests, MyPrivateMessages, MyNotifications, MyShares, MyReactions, MySharePost, EditMyPost, DelMyPost }