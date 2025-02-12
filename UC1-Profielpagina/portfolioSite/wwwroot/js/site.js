document.addEventListener("DOMContentLoaded", function() {
    const numBalls = 150;
    const balls = [];
    const ballSize = 5;
    const ballSpeed = 4;
    let mouseX = window.innerWidth / 2;
    let mouseY = window.innerHeight / 2;
    let followThreshold = 300;

    // Find the existing div with id "blue-ball"
    const existingBall = document.getElementById("blue-ball");

    function createBall() {
        const ball = existingBall.cloneNode(true); // Clone the existing ball
        ball.classList.add("ball");
        document.body.appendChild(ball);

        let x = Math.random() * (window.innerWidth - ballSize);
        let y = Math.random() * (window.innerHeight - ballSize);
        let speedX = (Math.random() - 0.5) * ballSpeed * 2;
        let speedY = (Math.random() - 0.5) * ballSpeed * 2;

        balls.push({ element: ball, x, y, speedX, speedY });
    }

    // Create the balls
    for (let i = 0; i < numBalls; i++) {
        createBall();
    }

    // Track mouse movement
    document.addEventListener("mousemove", (event) => {
        mouseX = event.clientX;
        mouseY = event.clientY;
    });

    // Update ball positions
    function updatePositions() {
        for (let ball of balls) {
            let dx = mouseX - (ball.x + ballSize / 2);
            let dy = mouseY - (ball.y + ballSize / 2);
            let distance = Math.sqrt(dx * dx + dy * dy);

            if (distance < followThreshold) {
                ball.speedX = dx * -0.02;
                ball.speedY = dy * -0.02;
            }

            ball.x += ball.speedX;
            ball.y += ball.speedY;

            if (ball.x <= 0 || ball.x >= window.innerWidth - ballSize) ball.speedX *= -1;
            if (ball.y <= 0 || ball.y >= window.innerHeight - ballSize) ball.speedY *= -1;

            ball.element.style.left = `${ball.x}px`;
            ball.element.style.top = `${ball.y}px`;
        }
        handleCollisions();
        requestAnimationFrame(updatePositions);
    }

    // Handle collisions between balls
    function handleCollisions() {
        for (let i = 0; i < balls.length; i++) {
            for (let j = i + 1; j < balls.length; j++) {
                let b1 = balls[i];
                let b2 = balls[j];
                let dx = b2.x - b1.x;
                let dy = b2.y - b1.y;
                let distance = Math.sqrt(dx * dx + dy * dy);

                if (distance < ballSize) {
                    let angle = Math.atan2(dy, dx);
                    let targetX = b1.x + Math.cos(angle) * ballSize;
                    let targetY = b1.y + Math.sin(angle) * ballSize;
                    let ax = (targetX - b2.x) * 0.1;
                    let ay = (targetY - b2.y) * 0.1;

                    b1.speedX -= ax;
                    b1.speedY -= ay;
                    b2.speedX += ax;
                    b2.speedY += ay;
                }
            }
        }
    }

    updatePositions();
});