function Stories() {
    if(document.querySelector('#mystoriesblk')) {
        document.querySelector('#mystoriesblk').innerHTML = `
        <div class="blkstories">
            <div class="row">
                <div class="col-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <img src="assets/images/default.webp" class="imgstories">
                    <div class="authorstories">
                        <span class="nameauthorstories">Luis Carvalho</span>
                        <img src="assets/images/luigi.png" class="imgauthorstories">
                    </div>
                </div>
                <div class="col-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <img src="assets/images/default.webp" class="imgstories">
                    <div class="authorstories">
                        <span class="nameauthorstories">Guest</span>
                        <img src="assets/images/guest.png" class="imgauthorstories guest">
                    </div>
                </div>
                <div class="col-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <img src="assets/images/default.webp" class="imgstories">
                    <div class="authorstories">
                        <span class="nameauthorstories">Luis Carvalho</span>
                        <img src="assets/images/luigi.png" class="imgauthorstories">
                    </div>
                </div>
                <div class="col-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <img src="assets/images/default.webp" class="imgstories">
                    <div class="authorstories">
                        <span class="nameauthorstories">Guest</span>
                        <img src="assets/images/guest.png" class="imgauthorstories guest">
                    </div>
                </div>
                <div class="col-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <img src="assets/images/default.webp" class="imgstories">
                    <div class="authorstories">
                        <span class="nameauthorstories">Luis Carvalho</span>
                        <img src="assets/images/luigi.png" class="imgauthorstories">
                    </div>
                </div>
                <div class="col-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <img src="assets/images/default.webp" class="imgstories">
                    <div class="authorstories">
                        <span class="nameauthorstories">Guest</span>
                        <img src="assets/images/guest.png" class="imgauthorstories guest">
                    </div>
                </div>
            </div>
        </div>`;
    }
}

export { Stories }