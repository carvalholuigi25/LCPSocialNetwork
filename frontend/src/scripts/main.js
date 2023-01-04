import '../scss/dep_styles.scss';
import '../scss/styles.scss';
import * as bootstrap from 'bootstrap';
import * as comp from '../components/index';
import { importAll, doLogin, doReg, doReqMsgForFields, doPrevAvatar, doPrevCover } from '../scripts/my_functions';

// importAll(require.context('../assets/fonts', true, /\.(woff|woff2|ttf|eot)$/));
importAll(require.context('../assets/files', true, /\.(7z|zip|rar)$/));
importAll(require.context('../assets/images', true, /\.(svg|png|jpe?g|gif|webp)$/));
importAll(require.context('../assets/videos', true, /\.(mp4|mov|flv)$/));
importAll(require.context('../assets/icons', true, /\.ico$/));

comp.SetNotifications();
comp.Navbar();
comp.Footer();
comp.toggleTheme();

doLogin();
doReg();
doReqMsgForFields();
doPrevAvatar();
doPrevCover();