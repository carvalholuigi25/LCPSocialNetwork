function Footer() {
    if(document.querySelector('#mycopyrightblk')) {
        document.querySelector('#mycopyrightblk').innerHTML = `
            <footer class="copyright my-col-bottom" id="copyright">
                <p>
                    &copy;${new Date().getUTCFullYear()} - LCP. Created by <a href="mailto:carvalholuigi25@gmail.com" target="_self">Luigi Carvalho</a>
                </p>
            </footer>
        `;
    }
}

export { Footer }