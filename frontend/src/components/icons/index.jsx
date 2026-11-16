const base = {
  fill: 'none',
  stroke: 'currentColor',
  strokeWidth: 2.2,
  strokeLinecap: 'round',
  strokeLinejoin: 'round',
  'aria-hidden': true,
};

/** Illustration icon for each kind of equipment. */
export function TypeIcon({ type, size = 40 }) {
  const p = { ...base, width: size, height: size, viewBox: '0 0 48 48', strokeWidth: 2.4 };
  switch (type) {
    case 'PROJECTOR':
      return (
        <svg {...p}>
          <rect x="5" y="16" width="38" height="17" rx="4" />
          <circle cx="32" cy="24.5" r="5" />
          <circle cx="32" cy="24.5" r="1.6" fill="currentColor" />
          <path d="M11 22h8M11 27h5M12 33v4M36 33v4" />
        </svg>
      );
    case 'HDMI_CABLE':
      return (
        <svg {...p}>
          <path d="M4 30h9l4-8h12a6 6 0 0 1 6 6v0" />
          <rect x="33" y="21" width="11" height="14" rx="2" />
          <path d="M37 21v-4h4v4M36 29h6" />
        </svg>
      );
    case 'REMOTE_CONTROL':
      return (
        <svg {...p}>
          <rect x="15" y="4" width="18" height="40" rx="9" />
          <circle cx="24" cy="15" r="3" />
          <path d="M20 25h8M20 31h8M20 37h8" />
        </svg>
      );
    case 'ALL':
      return (
        <svg {...p}>
          <rect x="6" y="6" width="15" height="15" rx="4" />
          <rect x="27" y="6" width="15" height="15" rx="4" />
          <rect x="6" y="27" width="15" height="15" rx="4" />
          <rect x="27" y="27" width="15" height="15" rx="4" />
        </svg>
      );
    default:
      return (
        <svg {...p}>
          <path d="M17 6v10M31 6v10M12 16h24v6a12 12 0 0 1-24 0zM24 34v8" />
        </svg>
      );
  }
}
