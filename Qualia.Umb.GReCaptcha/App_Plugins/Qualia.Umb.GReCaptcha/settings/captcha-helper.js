async function getReCaptchaToken() {
    return new Promise((res, rej) => {
        grecaptcha.ready(function () {
            grecaptcha.execute('reCAPTCHA_site_key', { action: 'submit' }).then((token) => {
                return res(token);
            })
        })
    });
}