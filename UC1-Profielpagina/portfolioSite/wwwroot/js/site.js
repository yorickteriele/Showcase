document.addEventListener("DOMContentLoaded", function() {
    const ball = document.getElementById('blue-ball');
    const maxX = window.innerWidth - ball.offsetWidth;
    const maxY = window.innerHeight - ball.offsetHeight;
    let x = Math.random() * maxX, y = Math.random() * maxY;
    let ballspeed = 0.5;
    let speedX = ballspeed, speedY = ballspeed;
    let mouseX = x, mouseY = y;
    let followThreshold = 100; 
    let followingMouse = false;

    document.addEventListener("mousemove", (event) => {
        mouseX = event.clientX;
        mouseY = event.clientY;
        followingMouse = true;
    });

    function moveBall() {
        let dx = mouseX - (x + ball.offsetWidth / 2);
        let dy = mouseY - (y + ball.offsetHeight / 2);
        let distance = Math.sqrt(dx * dx + dy * dy);

        if (distance < followThreshold) {
            x += dx * 0.05;
            y += dy * 0.05;
            speedX = dx * 0.05;
            speedY = dy * 0.05;
        } else {
            x += speedX;
            y += speedY;
        }

        if (x >= maxX || x <= 0) {
            speedX = -speedX;
            x = Math.max(0, Math.min(x, maxX));
        }
        if (y >= maxY || y <= 0) {
            speedY = -speedY;
            y = Math.max(0, Math.min(y, maxY));
        }

        ball.style.left = `${x}px`;
        ball.style.top = `${y}px`;

        requestAnimationFrame(moveBall);
    }

    moveBall();
});
