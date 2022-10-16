import '../scss/dep_styles.scss';
import '../scss/styles.scss';
import * as bootstrap from 'bootstrap';
import * as comp from '../components/index';
import { importAll } from '../scripts/my_functions';

// importAll(require.context('../assets/fonts', true, /\.(woff|woff2|ttf|eot)$/));
importAll(require.context('../assets/files', true, /\.(7z|zip|rar)$/));
importAll(require.context('../assets/images', true, /\.(svg|png|jpe?g|gif|webp)$/));
importAll(require.context('../assets/videos', true, /\.(mp4|mov|flv)$/));
importAll(require.context('../assets/icons', true, /\.ico$/));

comp.Navbar();
comp.Footer();
comp.toggleTheme();

if(document.querySelector('#reqmsg')) {
    document.querySelector('#reqmsg').innerHTML = `
    <p>
        <b>Note:</b> The field inputs marked in labels those with <span class="reqsym"></span> are required to fill!
    </p>`;
}

if(document.querySelector('#btnprevcover')) {
    document.querySelector('#cover').oninput = function(e) {
        document.querySelector('#blkprevcover .coverprev').src = "/assets/images/" + e.target.files[0].name;
    };

    document.querySelector('#btnprevcover').onclick = function(e) {
        e.preventDefault();
        if(document.querySelector('#blkprevcover').classList.contains('hidden')) {
            document.querySelector('#blkprevcover').classList.remove('hidden');
        } else {
            document.querySelector('#blkprevcover').classList.add('hidden');
        }
    };
}

if(document.querySelector('#btnprevavatar')) {
    document.querySelector('#avatar').oninput = function(e) {
        document.querySelector('#blkprevavatar .avatarprev').src = "/assets/images/" + e.target.files[0].name;
    };

    document.querySelector('#btnprevavatar').onclick = function(e) {
        e.preventDefault();
        if(document.querySelector('#blkprevavatar').classList.contains('hidden')) {
            document.querySelector('#blkprevavatar').classList.remove('hidden');
        } else {
            document.querySelector('#blkprevavatar').classList.add('hidden');
        }
    };
}