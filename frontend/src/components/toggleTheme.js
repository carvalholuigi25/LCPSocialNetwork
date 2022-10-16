function toggleTheme() {
    if(localStorage.getItem('theme')) {
        if(document.querySelectorAll('.theme')[0] && !document.querySelectorAll('.theme')[0].classList.contains(localStorage.getItem('theme'))) {
            document.querySelectorAll('.theme')[0].classList.add(localStorage.getItem('theme'));
        }
    
        if(document.querySelector('#togThemeDarkMode')) {
            if(localStorage.getItem('theme') == "dark") {
                document.querySelector('#togThemeDarkMode').setAttribute('checked', true);
            } else {
                document.querySelector('#togThemeDarkMode').removeAttribute('checked');
            }
        }
    }

    if(document.querySelector('#togThemeDarkMode')) {
        document.querySelector('#togThemeDarkMode').onchange = function() {
            if(document.querySelectorAll('.theme')[0]) {
                document.querySelectorAll('.theme')[0].classList.toggle('dark');

                if(document.querySelectorAll('.theme')[0].classList.contains('dark')) {
                    localStorage.setItem('theme', 'dark');
                    this.setAttribute('checked', true);
                } else {
                    localStorage.setItem('theme', 'default');
                    this.removeAttribute('checked');
                }
            }
        };
    }
}

export { toggleTheme }