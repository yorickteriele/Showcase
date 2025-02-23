class GDPR {
    constructor() {
        this.showStatus();
        this.showContent();
        this.bindEvents();
        
        
        if(!this.getCookie("gdpr-consent")) {
            this.showGDPR();
        }
    }

    bindEvents() {
        let buttonAccept = document.querySelector('.gdpr-consent__button--accept');
        buttonAccept.addEventListener('click', () => {
            this.cookieStatus('accept');
            this.showStatus();
            this.showContent();
            this.hideGDPR();
        });

        let buttonReject = document.querySelector('.gdpr-consent__button--reject');
        buttonReject.addEventListener('click', () => {
            this.cookieStatus('reject');
            this.showStatus();
            this.showContent();
            this.hideGDPR();
        });
    }

    showContent() {
        this.resetContent();
        const status = this.cookieStatus() == null ? 'not-chosen' : this.cookieStatus();
        const element = document.querySelector(`.content-gdpr-${status}`);
        if (element) {
            element.classList.add('show');
        }
    }

    resetContent() {
        const classes = [
            '.content-gdpr-accept',
            '.content-gdpr-reject',
            '.content-gdpr-not-chosen'
        ];

        for (const c of classes) {
            const element = document.querySelector(c);
            if (element) {
                element.classList.add('hide');
                element.classList.remove('show');
            }
        }
    }

    showStatus() {
        const statusElement = document.getElementById('content-gpdr-consent-status');
        if (statusElement) {
            statusElement.innerHTML = this.cookieStatus() == null ? 'Niet gekozen' : this.cookieStatus();
        }
    }

    cookieStatus(status) {
        if (status) {
            localStorage.setItem('gdpr-consent-choice', status);
            document.cookie = `gdpr-consent=${status}; path=/; max-age=31536000`; // Cookie geldig voor 1 jaar
        }
        return localStorage.getItem('gdpr-consent-choice') || this.getCookie('gdpr-consent');
    }

    getCookie(name) {
        const cookieName = name + "=";
        const cookies = document.cookie.split(';');
        for (let i = 0; i < cookies.length; i++) {
            let cookie = cookies[i].trim();
            if (cookie.indexOf(cookieName) === 0) {
                return cookie.substring(cookieName.length, cookie.length);
            }
        }
        return null;
    }

    hideGDPR() {
        const gdprConsent = document.querySelector('.gdpr-consent');
        if (gdprConsent) {
            gdprConsent.classList.add('hide');
            gdprConsent.classList.remove('show');
        }
    }

    showGDPR() {
        const gdprConsent = document.querySelector(".gdpr-consent");
        if (gdprConsent) {
            gdprConsent.classList.add('show');
            gdprConsent.classList.remove("hide");
        }
    }
}

document.addEventListener("DOMContentLoaded", function () {
    const gdpr = new GDPR();
});