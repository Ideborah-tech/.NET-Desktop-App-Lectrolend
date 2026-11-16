import { useEffect, useRef } from 'react';

/**
 * Lightweight 3D scene: projector + remote + cable.
 * - No RoomEnvironment / PMREMGenerator (saves ~200ms of GPU setup)
 * - Reduced geometry segments
 * - Single dynamic import of 'three' core only
 */
export default function Scene3D({ className = '', minWidth = 0, zoom = 1 }) {
  const mountRef = useRef(null);

  useEffect(() => {
    if (window.innerWidth < minWidth) return undefined;
    let disposed = false;
    let cleanup = () => {};

    import('three').then((THREE) => {
      import('three/examples/jsm/geometries/RoundedBoxGeometry.js').then(({ RoundedBoxGeometry }) => {
        if (disposed || !mountRef.current) return;
        cleanup = build(THREE, RoundedBoxGeometry, mountRef.current, zoom);
      });
    }).catch(() => {});

    return () => { disposed = true; cleanup(); };
  }, [minWidth, zoom]);

  return <div ref={mountRef} className={`scene3d ${className}`} aria-hidden="true" />;
}

const NAVY = 0x03346e;
const ACCENT = 0x2c74b3;
const SUN = 0xffc93c;
const PAPER = 0xeaf2fa;

function build(THREE, RoundedBoxGeometry, el, zoom) {
  const reduceMotion = window.matchMedia('(prefers-reduced-motion: reduce)').matches;

  let renderer;
  try {
    renderer = new THREE.WebGLRenderer({ antialias: true, alpha: true, powerPreference: 'default' });
  } catch { return () => {}; }

  renderer.setPixelRatio(Math.min(window.devicePixelRatio || 1, 1.5));
  el.appendChild(renderer.domElement);

  const scene = new THREE.Scene();
  const camera = new THREE.PerspectiveCamera(30, 1, 0.1, 100);
  camera.position.set(0.4, 0.9, 9.2);
  camera.lookAt(0, 0, 0);

  // Simple lights — no env map needed
  scene.add(new THREE.AmbientLight(0xffffff, 0.6));
  const key = new THREE.DirectionalLight(0xffffff, 1.4);
  key.position.set(3, 6, 6);
  scene.add(key);
  const fill = new THREE.DirectionalLight(SUN, 0.7);
  fill.position.set(-4, -2, 3);
  scene.add(fill);
  const rim = new THREE.DirectionalLight(ACCENT, 0.9);
  rim.position.set(4, 1, -3);
  scene.add(rim);

  // Materials — MeshLambertMaterial is much cheaper than MeshStandardMaterial
  const bodyMat  = new THREE.MeshLambertMaterial({ color: PAPER });
  const navyMat  = new THREE.MeshLambertMaterial({ color: NAVY });
  const sunMat   = new THREE.MeshLambertMaterial({ color: SUN });
  const accentMat = new THREE.MeshLambertMaterial({ color: ACCENT });
  const ledMat   = new THREE.MeshBasicMaterial({ color: SUN });

  // ---- projector
  const projector = new THREE.Group();
  projector.add(new THREE.Mesh(new RoundedBoxGeometry(3.3, 1.1, 2.3, 4, 0.28), bodyMat));

  const topStrip = new THREE.Mesh(new RoundedBoxGeometry(2.2, 0.06, 0.18, 2, 0.03), accentMat);
  topStrip.position.set(0.35, 0.56, 0.55);
  projector.add(topStrip);

  for (let i = 0; i < 5; i++) {
    const vent = new THREE.Mesh(new THREE.BoxGeometry(1.0, 0.02, 0.07), navyMat);
    vent.position.set(0.55, 0.555, -0.1 - i * 0.16);
    projector.add(vent);
  }

  const lens = new THREE.Group();
  const barrel = new THREE.Mesh(new THREE.CylinderGeometry(0.58, 0.64, 0.55, 32), navyMat);
  barrel.rotation.x = Math.PI / 2;
  lens.add(barrel);
  const ring = new THREE.Mesh(new THREE.TorusGeometry(0.6, 0.07, 12, 32), sunMat);
  ring.position.z = 0.28;
  lens.add(ring);
  const glass = new THREE.Mesh(new THREE.CylinderGeometry(0.44, 0.44, 0.06, 32), accentMat);
  glass.rotation.x = Math.PI / 2;
  glass.position.z = 0.27;
  lens.add(glass);
  lens.position.set(-0.75, 0.02, 1.4);
  projector.add(lens);

  const led = new THREE.Mesh(new THREE.SphereGeometry(0.08, 12, 12), ledMat);
  led.position.set(1.35, 0.1, 1.18);
  projector.add(led);

  [[-1.2, -0.7], [1.2, -0.7], [-1.2, 0.8], [1.2, 0.8]].forEach(([x, z]) => {
    const foot = new THREE.Mesh(new THREE.CylinderGeometry(0.12, 0.14, 0.14, 12), navyMat);
    foot.position.set(x, -0.62, z);
    projector.add(foot);
  });
  projector.rotation.set(0.12, -0.55, 0);
  projector.position.set(0, 0, 0);
  scene.add(projector);

  // ---- orbiters
  const remote = new THREE.Group();
  remote.add(new THREE.Mesh(new RoundedBoxGeometry(0.55, 1.75, 0.26, 4, 0.13), sunMat));
  for (let i = 0; i < 4; i++) {
    const b = new THREE.Mesh(new THREE.CylinderGeometry(0.08, 0.08, 0.04, 12), i === 0 ? accentMat : navyMat);
    b.rotation.x = Math.PI / 2;
    b.position.set(i % 2 ? 0.11 : -0.11, 0.55 - Math.floor(i / 2) * 0.28, 0.14);
    remote.add(b);
  }
  scene.add(remote);

  const cable = new THREE.Mesh(new THREE.TorusKnotGeometry(0.62, 0.09, 80, 10, 2, 3), accentMat);
  scene.add(cable);

  // ---- interaction + loop
  const mouse = { x: 0, y: 0, tx: 0, ty: 0 };
  const onMove = (e) => {
    mouse.tx = (e.clientX / window.innerWidth) * 2 - 1;
    mouse.ty = (e.clientY / window.innerHeight) * 2 - 1;
  };
  window.addEventListener('pointermove', onMove, { passive: true });

  const resize = () => {
    const w = el.clientWidth || 400;
    const h = el.clientHeight || 400;
    renderer.setSize(w, h);
    camera.aspect = w / h;
    camera.fov = 30;
    camera.position.z = (w / h < 0.9 ? 13.0 : w / h < 1.2 ? 11.0 : 10.2);
    camera.updateProjectionMatrix();
  };
  const ro = new ResizeObserver(resize);
  ro.observe(el);
  resize();

  let visible = true;
  const io = new IntersectionObserver(([e]) => { visible = e.isIntersecting; }, { threshold: 0 });
  io.observe(el);

  const clock = new THREE.Clock();
  let raf = 0;
  const frame = () => {
    raf = requestAnimationFrame(frame);
    if (!visible || document.hidden) return;
    const t = clock.getElapsedTime();
    mouse.x += (mouse.tx - mouse.x) * 0.05;
    mouse.y += (mouse.ty - mouse.y) * 0.05;

    projector.position.y = Math.sin(t * 0.9) * 0.14;
    projector.rotation.y = -0.55 + Math.sin(t * 0.35) * 0.3 + mouse.x * 0.45;
    projector.rotation.x = 0.12 + mouse.y * 0.18;

    remote.position.set(Math.cos(t * 0.55) * 2.55, Math.sin(t * 0.8) * 0.5 + 0.6, Math.sin(t * 0.55) * 1.6 - 0.6);
    remote.rotation.set(t * 0.4, t * 0.6, 0.35);
    cable.position.set(Math.cos(t * 0.5 + Math.PI) * 2.6, -0.9 + Math.sin(t * 0.7) * 0.35, Math.sin(t * 0.5 + Math.PI) * 1.8 - 0.4);
    cable.rotation.set(t * 0.5, t * 0.35, 0);
    renderer.render(scene, camera);
  };

  if (reduceMotion) renderer.render(scene, camera);
  else frame();

  return () => {
    cancelAnimationFrame(raf);
    ro.disconnect();
    io.disconnect();
    window.removeEventListener('pointermove', onMove);
    scene.traverse((o) => {
      if (o.geometry) o.geometry.dispose();
      if (o.material) (Array.isArray(o.material) ? o.material : [o.material]).forEach((m) => m.dispose());
    });
    renderer.dispose();
    renderer.forceContextLoss();
    renderer.domElement.remove();
  };
}
