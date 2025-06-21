document.addEventListener('DOMContentLoaded', function () {
    // Create floating academic elements
    const academicBg = document.querySelector('.academic-bg');
    const academicIcons = ['📚', '🎓', '✏️', '📝', '📖', '🏫', '📘', '📙', '📕', '📗'];

    // Create 15 floating academic elements
    for (let i = 0; i < 15; i++) {
        const element = document.createElement('div');
        element.className = 'academic-element';
        element.textContent = academicIcons[Math.floor(Math.random() * academicIcons.length)];

        // Random properties
        const size = Math.random() * 2 + 1.5;
        const delay = Math.random() * 10;
        const duration = 15 + Math.random() * 15;
        const left = Math.random() * 100;
        const opacity = 0.1 + Math.random() * 0.2;

        element.style.cssText = `
            font-size: ${size}rem;
            animation-delay: ${delay}s;
            animation-duration: ${duration}s;
            left: ${left}%;
            top: ${Math.random() * 100}%;
            opacity: ${opacity};
            transform: rotate(${Math.random() * 360}deg);
        `;

        academicBg.appendChild(element);
    }

    // Create floating books
    const floatingBooks = document.querySelector('.floating-books');
    for (let i = 0; i < 10; i++) {
        const book = document.createElement('div');
        book.className = 'floating-book';

        // Random properties
        const width = 30 + Math.random() * 30;
        const height = 40 + Math.random() * 40;
        const delay = Math.random() * 15;
        const duration = 15 + Math.random() * 20;
        const left = Math.random() * 100;
        const color = `hsl(${Math.random() * 360}, 70%, 60%)`;
        const opacity = 0.05 + Math.random() * 0.1;

        book.style.cssText = `
            width: ${width}px;
            height: ${height}px;
            background-color: ${color};
            animation-delay: ${delay}s;
            animation-duration: ${duration}s;
            left: ${left}%;
            opacity: ${opacity};
            transform: rotate(${Math.random() * 360}deg);
        `;

        floatingBooks.appendChild(book);
    }

    // Create logo particles
    const logoParticles = document.querySelector('.logo-particles');
    for (let i = 0; i < 3; i++) {
        const particle = document.createElement('div');
        particle.className = 'particle';

        const size = 6 + Math.random() * 4;
        const delay = Math.random() * 2;
        const duration = 3 + Math.random() * 2;
        const x = Math.random() * 20 - 10;
        const y = Math.random() * 20 - 10;

        particle.style.cssText = `
            width: ${size}px;
            height: ${size}px;
            animation-delay: ${delay}s;
            animation-duration: ${duration}s;
            left: ${50 + x}%;
            top: ${50 + y}%;
        `;

        logoParticles.appendChild(particle);
    }

    // Create particle network
    const particleNetwork = document.querySelector('.particle-network');
    const particles = [];
    const colors = ['rgba(67, 97, 238, 0.2)', 'rgba(247, 37, 133, 0.2)', 'rgba(76, 201, 240, 0.2)'];

    // Create particles
    for (let i = 0; i < 30; i++) {
        const particle = document.createElement('div');
        particle.className = 'network-particle';

        const size = 2 + Math.random() * 3;
        const x = Math.random() * 100;
        const y = Math.random() * 100;
        const color = colors[Math.floor(Math.random() * colors.length)];

        particle.style.cssText = `
            width: ${size}px;
            height: ${size}px;
            background: ${color};
            left: ${x}%;
            top: ${y}%;
            border-radius: 50%;
            position: absolute;
        `;

        particleNetwork.appendChild(particle);
        particles.push({
            element: particle,
            x: x,
            y: y,
            vx: Math.random() * 0.2 - 0.1,
            vy: Math.random() * 0.2 - 0.1
        });
    }

    // Animate particles
    function animateParticles() {
        particles.forEach(p => {
            p.x += p.vx;
            p.y += p.vy;

            // Bounce off edges
            if (p.x < 0 || p.x > 100) p.vx *= -1;
            if (p.y < 0 || p.y > 100) p.vy *= -1;

            p.element.style.left = `${p.x}%`;
            p.element.style.top = `${p.y}%`;
        });

        // Draw connections
        drawConnections();

        requestAnimationFrame(animateParticles);
    }

    // Draw connections between particles
    function drawConnections() {
        const canvas = document.createElement('canvas');
        const ctx = canvas.getContext('2d');
        canvas.width = window.innerWidth;
        canvas.height = window.innerHeight;
        canvas.style.position = 'fixed';
        canvas.style.top = '0';
        canvas.style.left = '0';
        canvas.style.zIndex = '-1';
        canvas.style.opacity = '0.3';

        // Clear previous canvas
        const oldCanvas = document.querySelector('.particle-canvas');
        if (oldCanvas) oldCanvas.remove();

        canvas.className = 'particle-canvas';
        document.body.appendChild(canvas);

        // Draw connections
        ctx.strokeStyle = 'rgba(67, 97, 238, 0.1)';
        ctx.lineWidth = 1;

        for (let i = 0; i < particles.length; i++) {
            for (let j = i + 1; j < particles.length; j++) {
                const p1 = particles[i];
                const p2 = particles[j];

                const x1 = p1.x / 100 * canvas.width;
                const y1 = p1.y / 100 * canvas.height;
                const x2 = p2.x / 100 * canvas.width;
                const y2 = p2.y / 100 * canvas.height;

                const distance = Math.sqrt(Math.pow(x2 - x1, 2) + Math.pow(y2 - y1, 2));

                if (distance < 150) {
                    ctx.beginPath();
                    ctx.moveTo(x1, y1);
                    ctx.lineTo(x2, y2);
                    ctx.stroke();
                }
            }
        }
    }

    // Start animation
    animateParticles();
});