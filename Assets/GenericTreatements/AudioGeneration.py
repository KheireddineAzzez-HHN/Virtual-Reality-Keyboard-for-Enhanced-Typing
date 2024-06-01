from pydub import AudioSegment
import string
import os

def change_pitch(sound, semitones):
    """Change the pitch of the given sound by the specified number of semitones."""
    return sound._spawn(sound.raw_data, overrides={
        "frame_rate": int(sound.frame_rate * (2.0 ** (semitones / 12.0)))
    }).set_frame_rate(sound.frame_rate)

def generate_and_save_sounds(click_sound_path, duration_ms=200):
    # Load the click sound
    click_sound = AudioSegment.from_mp3(click_sound_path)

    # Define the duration for each key sound in milliseconds
    key_sounds = {}
    for i, char in enumerate(string.ascii_lowercase):
        output_path = f"../Resources/audio/{char}_click_key.mp3"
        
        # Change pitch for each key based on its position in the alphabet
        if not os.path.exists(output_path):
            pitch_shift = i - 13  # Shift from -13 semitones for 'a' to +12 semitones for 'z'
            modified_sound = change_pitch(click_sound[:duration_ms], pitch_shift)
            modified_sound.export(output_path, format="mp3")
        key_sounds[char] = output_path

    return key_sounds

# Update with your click sound path
click_sound_path = "import/audio/click_segment.mp3"

# Generate and save sounds
key_sounds = generate_and_save_sounds(click_sound_path)

# Output the paths to the generated sounds
print("Generated click-based sounds with modified pitch:")
for char, path in key_sounds.items():
    print(f"{char}: {path}")

