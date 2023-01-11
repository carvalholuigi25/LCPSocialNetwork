import { getMyCurToken, getMyQueryVal, Read } from ".";

function getDataUT(tblname) {
    if(tblname.length > 0) {
        tblname.forEach(tblval => {
            if(document.querySelector('#blklistft'+tblval)) {
                Read(tblval, -1, getMyCurToken()).then(x => {
                    if(x != null && x.length > 0) {
                        x.forEach(xd => {
                            if(getMyQueryVal().id == xd[tblval.replace("s", "")+"Id"]) {
                                document.querySelector('#blklistft'+tblval).innerHTML += `
                                    <div class="col-12 card carddet mb-3">
                                        <div class="mimgbgcover">
                                            <img src="${xd.cover}" class="card-img-top img-fluid imgbgcover" alt="${xd.title}" title="${xd.title}" />
                                        </div>
                                        <div class="mimgcover">
                                            <img src="${xd.mainImage}" class="card-img-top img-fluid imgcover" alt="${xd.title}" title="${xd.title}" />
                                        </div>
                                        <div class="card-body">
                                            <h3 class="card-title">${xd.title}</h3>
                                            <p class="card-text">
                                                ${xd.dateStart} || ${xd.company} || ${xd.distributor} || ${xd.publisher}
                                            </p>
                                            <p class="card-text">${xd.text ?? xd.desc}</p>
                                            <p class="card-text">Rating: ${xd.rating} / 10</p>
                                            <p class="card-text">Favorite: ${xd.isFavorite}</p>
                                            <a href="pages/userthings/${tblval.toLowerCase()}.html" class="btn btn-primary">
                                                Back
                                            </a>
                                        </div>
                                    </div>
                                `;
                            } else {
                                document.querySelector('#blklistft'+tblval).innerHTML += `
                                    <div class="col-12 col-md-6 col-lg-4 col-xl-2 col-xxl-2 card">
                                        <a href="pages/userthings/${tblval.toLowerCase()}.html?id=${xd.gameId}">
                                            <img src="${xd.mainImage}" class="card-img-top img-fluid" alt="${xd.title}" title="${xd.title}" />
                                        </a>
                                    </div>
                                `;
                            }
                        });
                    } else {
                        document.querySelector('#blklistft'+tblval).innerHTML = `
                            <div class="warnblk">
                                <i class="bi bi-exclamation-circle" style="font-size: 4rem; color: red;"></i>
                                <h3>0 ${tblval}!</h3>
                            </div>
                        `;
                    }
                }).catch(err => console.log(err));
            }
        });
    }
}

getDataUT(["games", "movies", "tvseries", "animes", "mangas", "comicbooks"]);

export { getDataUT }