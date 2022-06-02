using Godot;

namespace UI
{
    public class OptionsMenu : Control
    {
        private Button _backButton;
        private Slider _volumeSlider;
        //private Button _bassBoostCheckButton;

        public override void _Ready()
        {
            _volumeSlider = GetNode<Slider>("Panel/VBoxContainer/HBoxContainer/VolumeSlider");
            //_bassBoostCheckButton = GetNode<Button>("Panel/VBoxContainer/HBoxContainer2/BassBoostCheckButton");
            _backButton = GetNode<Button>("Panel/BackButton");
        }

        private void BackButtonClicked()
        {
            if (_backButton.Pressed)
            {
                GetParent().GetNode<Control>("OptionsMenu").Visible = false;
                if (GetParent().GetNode<PanelContainer>("PauseContainer") != null)
                    GetParent().GetNode<PanelContainer>("PauseContainer").Visible = true;
            }
        }

        private void VolumeSliderChanged(float Value)
        {
            AudioServer.SetBusVolumeDb(
                AudioServer.GetBusIndex("Master"),
                (float)_volumeSlider.Value
            );
            GD.Print(Value);
            if (Value == -24f)
            {
                AudioServer.SetBusMute(AudioServer.GetBusIndex("Master"),true);
            }
            else
            {
                AudioServer.SetBusMute(AudioServer.GetBusIndex("Master"),false);
            }
        }
    }
}
