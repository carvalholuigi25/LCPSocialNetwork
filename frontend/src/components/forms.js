import { Update, Delete, Read } from '../components/crud';
import { getMyApiUrl, fetchingData } from '../scripts/my_functions';
import { getMyCurId, getMyQueryVal, getMyCurToken } from './geral';
import { GetNotifications } from './notifications';
import { Posts } from '../classes/posts';

function getEditForm() {
    if(document.querySelector("#myeditpostform")) {
        document.querySelector("#myeditpostform").innerHTML = `
        <form method="put" class="frmeditpost p-3" id="frmeditpost">
            <div class="form-group mt-0">
                <label>Posts Id</label>
                <input name="postsid" type="number" class="form-control postsid" id="postsid" placeholder="Select your number for post id" readonly />
            </div>
            <div class="form-group mt-3">
                <label>Title</label>
                <input type="text" name="title" class="form-control title" id="title" placeholder="Write your title here" />
            </div>
            <div class="form-group mt-3">
                <label>Text</label>
                <textarea name="text" class="form-control text" id="text" cols="1" rows="10" placeholder="Write your text here"></textarea>
            </div>
            <div class="form-group mt-3">
                <label>Users Id</label>
                <input name="usersid" type="number" class="form-control usersid" id="usersid" placeholder="Select your number for user id" readonly />
            </div>
            <div class="form-group mt-3">
                <input type="reset" class="btn btn-secondary btnreset" id="btnreset" value="Clear" style="padding: 8px 1.5rem !important;" />
                <input type="button" class="btn btn-info btnresetdef" id="btnresetdef" value="Reset to default values" style="padding: 8px 1.5rem !important;" />
                <input type="submit" class="btn btn-primary btnsubmit" id="btnsubmit" value="Update" style="padding: 8px 1.5rem !important;" />
            </div>
            <div class="form-group mt-3">
                <a href="pages/main.html" class="btn btn-primary" style="padding: 8px 1.5rem !important;">Cancel</a>
            </div>
        </form>`;

        if(document.querySelector('#frmeditpost')) {
            var postid = getMyQueryVal().id ?? 1;
            var usersid = getMyQueryVal().usersId ?? 1;
            var token = getMyCurToken();

            Read("posts", postid, token).then(x => {
                if(x != null) {
                    document.querySelector('#frmeditpost #postsid').value = x.postId;
                    document.querySelector('#frmeditpost #title').value = x.title;
                    document.querySelector('#frmeditpost #text').value = x.text;
                    document.querySelector('#frmeditpost #usersid').value = x.usersId;
                }
            }).catch(err => console.log(err));

            document.querySelector('#frmeditpost #btnreset').onclick = function(e) {
                e.preventDefault();
                document.querySelector('#frmeditpost').reset();
            };

            document.querySelector('#frmeditpost #btnresetdef').onclick = function(e) {
                e.preventDefault();
                document.querySelector('#frmeditpost').reset();
                Read("posts", postid, token).then(x => {
                    if(x != null) {
                        document.querySelector('#frmeditpost #postsid').value = x.postId;
                        document.querySelector('#frmeditpost #title').value = x.title;
                        document.querySelector('#frmeditpost #text').value = x.text;
                        document.querySelector('#frmeditpost #usersid').value = x.usersId;
                    }
                }).catch(err => console.log(err));
            };

            document.querySelector('#frmeditpost').onsubmit = function(e) {
                e.preventDefault();

                var postsobj = {
                    postId: postid ?? document.querySelector('#frmeditpost #postsid').value,
                    title: document.querySelector("#frmeditpost #title").value,
                    shortdesc: document.querySelector('#frmeditpost #text').value,
                    text: document.querySelector("#frmeditpost #text").value,
                    image: "/assets/images/c_luigi.png",
                    attachments: null,
                    status: "published",
                    privacy: "publictxt",
                    isFeatured: false,
                    dateCreated: new Date().toISOString(),
                    dateModified: null,
                    dateDeleted: null,
                    usersId: usersid ?? document.querySelector('#frmeditpost #usersid').value,
                    reactsId: 1
                };

                Update("posts", postid, postsobj, token).then(x => {
                    GetNotifications("blksubnotifications", "text-bg-success", "bi-check-lg", "Updated post successfully!", true, 1000);
                    setTimeout(() => {
                        location.href = "pages/main.html";
                    }, 1000);
                }).catch(err => {
                    console.log(err);
                    GetNotifications("blksubnotifications", "text-bg-error", "bi-x", "Error: Could not to update post!", true, 1000);
                });
            }
        }
    }
}

function getDelForm() {
    if(document.querySelector("#mydelpostform")) {
        var postsdata = "";
        var postid = getMyQueryVal().id ?? 1;
        var token = getMyCurToken();

        Read("posts", postid, token).then(x => {
           if(x != null) {
                postsdata += `
                <tr>
                    <td>${x.postId}</td>
                    <td>${x.title}</td>
                    <td class="text-long">${x.text}</td>
                    <td>${x.usersId}</td>
                </tr>`;

                document.querySelector("#mydelpostform").innerHTML = `
                <form method="delete" class="frmdelpost p-3" id="frmdelpost">
                    <h4>Do you want to delete this post (id: ${postid})?</h4>
                    <div class="table-responsive mt-3">
                        <table class="table table-bordered table-info table-striped">
                            <thead>
                                <tr>
                                    <th>Id</th>
                                    <th>Title</th>
                                    <th>Text</th>
                                    <th>Users Id</th>
                                </tr>
                            </thead>
                            <tbody>
                                ${postsdata}
                            </tbody>
                        </table>
                    </div>
                    <div class="form-group mt-3">
                        <button type="submit" class="btn btn-primary btndelete" id="btndelete" style="padding: 8px 1.5rem !important;">Yes</button>
                        <button type="button" class="btn btn-secondary btncancel" id="btncancel" style="padding: 8px 1.5rem !important;">No</button>
                    </div>
                </form>`;

                if(document.querySelector('#frmdelpost')) {
                    document.querySelector('#frmdelpost #btncancel').onclick = function(e) {
                        e.preventDefault();

                        setTimeout(() => {
                            location.href = "pages/main.html";
                        }, 500);
                    };

                    document.querySelector('#frmdelpost').onsubmit = function(e) {
                        e.preventDefault();
        
                        Delete("posts", postid, token).then(x => {
                            GetNotifications("blksubnotifications", "text-bg-success", "bi-check-lg", "Deleted post successfully!", true, 1000);
                            setTimeout(() => {
                                location.href = "pages/main.html";
                            }, 1000);
                        }).catch(err => {
                            console.log(err);
                            GetNotifications("blksubnotifications", "text-bg-error", "bi-x", "Error: Could not to delete post!", true, 1000);
                        });
                    };
                }
           }
        }).catch(err => console.log(err));
    }
}

export { getEditForm, getDelForm }