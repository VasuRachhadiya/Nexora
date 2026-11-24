/**
 * NEXORA — Futuristic Web Experience
 * Interactive Particle Canvas | Scroll Reveals | 3D Tilt | Carousels | Toasts
 */

document.addEventListener('DOMContentLoaded', () => {
    initParticleCanvas();
    initScrollReveal();
    initCounterAnimations();
    initNavbarScroll();
    initCardTilt();
    initTestimonialCarousel();
    initSearchModal();
    initNewsletterForm();
    initMobileNav();
    initExploreFilters();
});

/* -------------------------------------------------------------
 * 1. Ambient Geometric Particle Canvas
 * ------------------------------------------------------------- */
function initParticleCanvas() {
    const canvas = document.getElementById('particle-canvas');
    if (!canvas) return;

    const ctx = canvas.getContext('2d');
    let width = canvas.width = window.innerWidth;
    let height = canvas.height = window.innerHeight;

    const particles = [];
    const particleCount = Math.min(Math.floor((width * height) / 18000), 75);
    const connectionDistance = 140;

    let mouse = { x: -1000, y: -1000, radius: 160 };

    window.addEventListener('mousemove', (e) => {
        mouse.x = e.clientX;
        mouse.y = e.clientY;
    });

    window.addEventListener('resize', () => {
        width = canvas.width = window.innerWidth;
        height = canvas.height = window.innerHeight;
    });

    class Particle {
        constructor() {
            this.x = Math.random() * width;
            this.y = Math.random() * height;
            this.vx = (Math.random() - 0.5) * 0.45;
            this.vy = (Math.random() - 0.5) * 0.45;
            this.radius = Math.random() * 1.8 + 0.8;
            this.baseAlpha = Math.random() * 0.5 + 0.2;
            this.color = Math.random() > 0.4 ? '139, 92, 246' : '6, 182, 212';
        }

        update() {
            this.x += this.vx;
            this.y += this.vy;

            if (this.x < 0) this.x = width;
            if (this.x > width) this.x = 0;
            if (this.y < 0) this.y = height;
            if (this.y > height) this.y = 0;

            // Subtle mouse repulsion
            const dx = mouse.x - this.x;
            const dy = mouse.y - this.y;
            const dist = Math.sqrt(dx * dx + dy * dy);
            if (dist < mouse.radius) {
                const angle = Math.atan2(dy, dx);
                const force = (mouse.radius - dist) / mouse.radius;
                this.x -= Math.cos(angle) * force * 1.5;
                this.y -= Math.sin(angle) * force * 1.5;
            }
        }

        draw() {
            ctx.beginPath();
            ctx.arc(this.x, this.y, this.radius, 0, Math.PI * 2);
            ctx.fillStyle = `rgba(${this.color}, ${this.baseAlpha})`;
            ctx.fill();
        }
    }

    for (let i = 0; i < particleCount; i++) {
        particles.push(new Particle());
    }

    function animate() {
        ctx.clearRect(0, 0, width, height);

        for (let i = 0; i < particles.length; i++) {
            particles[i].update();
            particles[i].draw();

            for (let j = i + 1; j < particles.length; j++) {
                const dx = particles[i].x - particles[j].x;
                const dy = particles[i].y - particles[j].y;
                const dist = Math.sqrt(dx * dx + dy * dy);

                if (dist < connectionDistance) {
                    const alpha = (1 - dist / connectionDistance) * 0.18;
                    ctx.beginPath();
                    ctx.moveTo(particles[i].x, particles[i].y);
                    ctx.lineTo(particles[j].x, particles[j].y);
                    ctx.strokeStyle = `rgba(139, 92, 246, ${alpha})`;
                    ctx.lineWidth = 0.8;
                    ctx.stroke();
                }
            }
        }

        requestAnimationFrame(animate);
    }

    animate();
}

/* -------------------------------------------------------------
 * 2. IntersectionObserver Scroll Reveal
 * ------------------------------------------------------------- */
function initScrollReveal() {
    const reveals = document.querySelectorAll('.reveal-init');
    if (!reveals.length) return;

    const observer = new IntersectionObserver((entries, obs) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('revealed');
                obs.unobserve(entry.target);
            }
        });
    }, {
        threshold: 0.12,
        rootMargin: '0px 0px -40px 0px'
    });

    reveals.forEach(el => observer.observe(el));
}

/* -------------------------------------------------------------
 * 3. Animated Statistics Counters
 * ------------------------------------------------------------- */
function initCounterAnimations() {
    const counters = document.querySelectorAll('.counter-val');
    if (!counters.length) return;

    const observer = new IntersectionObserver((entries, obs) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                const el = entry.target;
                const target = parseInt(el.getAttribute('data-target') || '0', 10);
                const suffix = el.getAttribute('data-suffix') || '';
                const prefix = el.getAttribute('data-prefix') || '';
                const duration = 2000;
                const startTime = performance.now();

                function updateCounter(currentTime) {
                    const elapsed = currentTime - startTime;
                    const progress = Math.min(elapsed / duration, 1);
                    // easeOutExpo
                    const easedProgress = progress === 1 ? 1 : 1 - Math.pow(2, -10 * progress);
                    const currentVal = Math.floor(easedProgress * target);

                    let formatted = currentVal.toLocaleString();
                    if (target >= 1000 && suffix.includes('K')) {
                        formatted = (currentVal / 1000).toFixed(0) + 'K';
                    }

                    el.textContent = `${prefix}${formatted}${suffix}`;

                    if (progress < 1) {
                        requestAnimationFrame(updateCounter);
                    } else {
                        let finalFormatted = target.toLocaleString();
                        if (target >= 1000 && suffix.includes('K')) {
                            finalFormatted = (target / 1000).toFixed(0) + 'K';
                        }
                        el.textContent = `${prefix}${finalFormatted}${suffix}`;
                    }
                }

                requestAnimationFrame(updateCounter);
                obs.unobserve(el);
            }
        });
    }, { threshold: 0.3 });

    counters.forEach(c => observer.observe(c));
}

/* -------------------------------------------------------------
 * 4. Sticky Navbar on Scroll
 * ------------------------------------------------------------- */
function initNavbarScroll() {
    const navbar = document.querySelector('.nexora-navbar');
    if (!navbar) return;

    window.addEventListener('scroll', () => {
        if (window.scrollY > 40) {
            navbar.classList.add('scrolled');
        } else {
            navbar.classList.remove('scrolled');
        }
    });
}

/* -------------------------------------------------------------
 * 5. 3D Card Tilt Interaction
 * ------------------------------------------------------------- */
function initCardTilt() {
    // Only run on non-touch devices
    if (window.matchMedia('(pointer: coarse)').matches) return;

    const cards = document.querySelectorAll('[data-tilt]');
    cards.forEach(card => {
        card.addEventListener('mousemove', (e) => {
            const rect = card.getBoundingClientRect();
            const x = e.clientX - rect.left;
            const y = e.clientY - rect.top;

            const centerX = rect.width / 2;
            const centerY = rect.height / 2;

            const rotateX = ((y - centerY) / centerY) * -6;
            const rotateY = ((x - centerX) / centerX) * 6;

            card.style.transform = `perspective(1000px) rotateX(${rotateX}deg) rotateY(${rotateY}deg) translateY(-4px)`;
        });

        card.addEventListener('mouseleave', () => {
            card.style.transform = 'perspective(1000px) rotateX(0deg) rotateY(0deg) translateY(0)';
        });
    });
}

/* -------------------------------------------------------------
 * 6. Testimonial Carousel
 * ------------------------------------------------------------- */
function initTestimonialCarousel() {
    const container = document.querySelector('.testimonial-carousel-wrap');
    if (!container) return;

    const slides = container.querySelectorAll('.testimonial-slide');
    const dots = container.querySelectorAll('.carousel-dot');
    const prevBtn = container.querySelector('.carousel-prev');
    const nextBtn = container.querySelector('.carousel-next');

    if (!slides.length) return;

    let currentIndex = 0;
    let timer = null;

    function showSlide(index) {
        if (index < 0) index = slides.length - 1;
        if (index >= slides.length) index = 0;
        currentIndex = index;

        slides.forEach((s, idx) => {
            s.style.display = idx === currentIndex ? 'block' : 'none';
            s.classList.toggle('active', idx === currentIndex);
        });

        dots.forEach((d, idx) => {
            d.classList.toggle('active', idx === currentIndex);
        });
    }

    function nextSlide() {
        showSlide(currentIndex + 1);
    }

    function prevSlide() {
        showSlide(currentIndex - 1);
    }

    if (nextBtn) nextBtn.addEventListener('click', () => { nextSlide(); resetTimer(); });
    if (prevBtn) prevBtn.addEventListener('click', () => { prevSlide(); resetTimer(); });

    dots.forEach((dot, idx) => {
        dot.addEventListener('click', () => {
            showSlide(idx);
            resetTimer();
        });
    });

    function startTimer() {
        timer = setInterval(nextSlide, 7000);
    }

    function resetTimer() {
        clearInterval(timer);
        startTimer();
    }

    showSlide(0);
    startTimer();
}

/* -------------------------------------------------------------
 * 7. Search Modal Overlay (Ctrl + K)
 * ------------------------------------------------------------- */
function initSearchModal() {
    const modal = document.querySelector('.nexora-search-modal');
    const openBtns = document.querySelectorAll('[data-open-search]');
    const closeBtns = document.querySelectorAll('[data-close-search]');
    const input = document.getElementById('search-modal-input');

    if (!modal) return;

    function openModal() {
        modal.classList.add('active');
        document.body.style.overflow = 'hidden';
        setTimeout(() => input?.focus(), 150);
    }

    function closeModal() {
        modal.classList.remove('active');
        document.body.style.overflow = '';
    }

    openBtns.forEach(btn => btn.addEventListener('click', (e) => {
        e.preventDefault();
        openModal();
    }));

    closeBtns.forEach(btn => btn.addEventListener('click', closeModal));

    modal.addEventListener('click', (e) => {
        if (e.target === modal) closeModal();
    });

    document.addEventListener('keydown', (e) => {
        if ((e.ctrlKey || e.metaKey) && e.key === 'k') {
            e.preventDefault();
            if (modal.classList.contains('active')) {
                closeModal();
            } else {
                openModal();
            }
        }
        if (e.key === 'Escape' && modal.classList.contains('active')) {
            closeModal();
        }
    });
}

/* -------------------------------------------------------------
 * 8. Newsletter AJAX Subscription & Toasts
 * ------------------------------------------------------------- */
function initNewsletterForm() {
    const forms = document.querySelectorAll('.newsletter-ajax-form');
    forms.forEach(form => {
        form.addEventListener('submit', async (e) => {
            e.preventDefault();
            const input = form.querySelector('input[type="email"]');
            const button = form.querySelector('button[type="submit"]');
            const email = input?.value?.trim();

            if (!email) return;

            const originalBtnText = button ? button.innerHTML : '';
            if (button) {
                button.disabled = true;
                button.innerHTML = '<span class="spinner-border spinner-border-sm"></span> Joining...';
            }

            try {
                const response = await fetch('/Newsletter/Subscribe', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded'
                    },
                    body: new URLSearchParams({ email })
                });

                const data = await response.json();
                if (data.success) {
                    showToast(data.message || 'Subscribed successfully!', 'success');
                    if (input) input.value = '';
                } else {
                    showToast(data.message || 'Could not subscribe. Please try again.', 'error');
                }
            } catch (err) {
                showToast('Network error. Please try again later.', 'error');
            } finally {
                if (button) {
                    button.disabled = false;
                    button.innerHTML = originalBtnText;
                }
            }
        });
    });
}

/* -------------------------------------------------------------
 * Toast Notification System
 * ------------------------------------------------------------- */
function showToast(message, type = 'info') {
    let container = document.querySelector('.nexora-toast-container');
    if (!container) {
        container = document.createElement('div');
        container.className = 'nexora-toast-container';
        document.body.appendChild(container);
    }

    const toast = document.createElement('div');
    toast.className = 'nexora-toast';

    let iconSvg = '';
    if (type === 'success') {
        iconSvg = `<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="#10b981" stroke-width="2.5"><path d="M20 6L9 17l-5-5"/></svg>`;
    } else if (type === 'error') {
        iconSvg = `<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="#f43f5e" stroke-width="2.5"><circle cx="12" cy="12" r="10"/><path d="M15 9l-6 6M9 9l6 6"/></svg>`;
    } else {
        iconSvg = `<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="#8b5cf6" stroke-width="2.5"><circle cx="12" cy="12" r="10"/><path d="M12 16v-4M12 8h.01"/></svg>`;
    }

    toast.innerHTML = `
        <div style="flex-shrink:0;">${iconSvg}</div>
        <div style="flex:1; font-size:0.92rem; font-weight:500;">${message}</div>
    `;

    container.appendChild(toast);

    setTimeout(() => {
        toast.style.opacity = '0';
        toast.style.transform = 'translateY(10px)';
        toast.style.transition = '0.4s ease';
        setTimeout(() => toast.remove(), 400);
    }, 4500);
}

/* -------------------------------------------------------------
 * 9. Mobile Navigation Drawer
 * ------------------------------------------------------------- */
function initMobileNav() {
    const toggle = document.querySelector('.mobile-nav-toggle');
    const overlay = document.querySelector('.mobile-drawer-overlay');
    const closeBtn = document.querySelector('.mobile-drawer-close');

    if (!toggle || !overlay) return;

    toggle.addEventListener('click', () => {
        overlay.classList.add('open');
        document.body.style.overflow = 'hidden';
    });

    function closeDrawer() {
        overlay.classList.remove('open');
        document.body.style.overflow = '';
    }

    if (closeBtn) closeBtn.addEventListener('click', closeDrawer);
    overlay.addEventListener('click', (e) => {
        if (e.target === overlay) closeDrawer();
    });
}

/* -------------------------------------------------------------
 * 10. Explore Categories Filter
 * ------------------------------------------------------------- */
function initExploreFilters() {
    const filterBtns = document.querySelectorAll('[data-explore-filter]');
    const cards = document.querySelectorAll('[data-experience-category]');

    if (!filterBtns.length || !cards.length) return;

    filterBtns.forEach(btn => {
        btn.addEventListener('click', (e) => {
            e.preventDefault();
            filterBtns.forEach(b => b.classList.remove('active'));
            btn.classList.add('active');

            const category = btn.getAttribute('data-explore-filter');

            cards.forEach(card => {
                const cardCat = card.getAttribute('data-experience-category');
                if (category === 'All' || cardCat === category) {
                    card.style.display = 'block';
                    setTimeout(() => {
                        card.style.opacity = '1';
                        card.style.transform = 'translateY(0)';
                    }, 50);
                } else {
                    card.style.opacity = '0';
                    card.style.transform = 'translateY(20px)';
                    setTimeout(() => {
                        card.style.display = 'none';
                    }, 250);
                }
            });
        });
    });
}
